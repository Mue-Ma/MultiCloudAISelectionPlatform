using MultiCloudAISelectionPlatform.Common.Entities;
using MultiCloudAISelectionPlatform.Common.Enums;
using MultiCloudAISelectionPlatform.Logic.Metrics;
using System.Reflection;

namespace MultiCloudAISelectionPlatform.Logic
{
    public static class MetricService
    {
        internal static MetricsResult[] GetMetrics(Services service, Providers[] providers)
        {
            var providerT = GetMetricsProviderClass(service);
            var providerInstance = Activator.CreateInstance(providerT, service) as MetricProviderBase ?? throw new Exception($"Provider for {service} couldn't be created!");

            providerInstance.StartAnalyzingServices(providers);

            return [.. providerInstance.MetricsResults];
        }

        private static Type GetMetricsProviderClass(Services service)
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t =>
                t.IsClass &&
                t.IsSubclassOf(typeof(MetricProviderBase)) &&
                t.Namespace == $"MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.{service}").First() ?? throw new Exception($"Provider for {service} not found!");
        }
    }
}
