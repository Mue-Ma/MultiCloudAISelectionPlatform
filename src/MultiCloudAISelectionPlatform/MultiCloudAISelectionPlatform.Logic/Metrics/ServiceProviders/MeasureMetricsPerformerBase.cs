using MultiCloudAISelectionPlatform.Common.Entities;
using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders
{
    internal abstract class MeasureMetricsPerformerBase(Providers provider)
    {
        public Providers Provider { get; private set; } = provider;

        public virtual async Task<MetricsResult> PerformMesurement(Services measuredService)
        {
            return await Task.Run(() =>
            {
                return new MetricsResult()
                {
                    Provider = Provider,
                    Service = measuredService,
                    StaticMetrics = new StaticMetrics() { Costs = GetCostsMeasure() },
                    DynamicMetrics = new DynamicMetrics()
                    {
                        Accuracy = GetAccuracyMeasure(),
                        ResponseTime = GetResponseTimeMeasure()
                    }
                };
            });
        }

        protected abstract decimal GetCostsMeasure();
        protected abstract double GetAccuracyMeasure();
        protected abstract double GetResponseTimeMeasure();
    }
}
