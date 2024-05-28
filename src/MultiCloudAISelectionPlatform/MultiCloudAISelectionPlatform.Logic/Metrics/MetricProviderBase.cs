using MultiCloudAISelectionPlatform.Common.Entities;
using MultiCloudAISelectionPlatform.Common.Enums;
using MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders;
using System.Reflection;

namespace MultiCloudAISelectionPlatform.Logic.Metrics
{
    public abstract class MetricProviderBase(Services measuredService)
    {
        public Services MeasuredService { get; private set; } = measuredService;
        public List<MetricsResult> MetricsResults { get; private set; } = [];

        public virtual void StartAnalyzingServices(Providers[] providers)
        {
            var metricProviderClasses = GetMetricProviderClasses();

            List<Task> metrikMeasureTasks = [];

            foreach (var providerT in metricProviderClasses)
            {
                if (providerT != null && providers.Any(p => p.ToString().Equals(providerT.Name)))
                {
                    if (Activator.CreateInstance(providerT) is MeasureMetricsPerformerBase instance)
                    {
                        metrikMeasureTasks.Add(Task.Run(async () =>
                        {
                            MetricsResults.Add(await instance.PerformMesurement(MeasuredService));
                        }));
                    }
                }
            }
            Task.WaitAll([.. metrikMeasureTasks]);
        }

        private Type[] GetMetricProviderClasses()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t =>
                t.IsClass &&
                t.IsSubclassOf(typeof(MeasureMetricsPerformerBase)) &&
                t.Namespace == $"MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.{MeasuredService}").ToArray();
        }
    }
}
