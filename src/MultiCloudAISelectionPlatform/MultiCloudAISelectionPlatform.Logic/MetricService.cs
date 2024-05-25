using MultiCloudAISelectionPlatform.Common.Entities;
using MultiCloudAISelectionPlatform.Common.Models;
using MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator;

namespace MultiCloudAISelectionPlatform.Logic
{
    public static class MetricService
    {
        public static StaticMetrics[] GetStaticMetrics()
        {
            throw new NotImplementedException();
        }

        public static DynamicMetrics[] GetDynamicMetrics()
        {
            throw new NotImplementedException();
        }

        internal static MetricsResult[] GetTranslatorMetrics()
        {
            TranslatorMetricsProvider provider = new();

            provider.StartAnalyzingServices();

            return [.. provider.MetricsResults];
        }
    }
}
