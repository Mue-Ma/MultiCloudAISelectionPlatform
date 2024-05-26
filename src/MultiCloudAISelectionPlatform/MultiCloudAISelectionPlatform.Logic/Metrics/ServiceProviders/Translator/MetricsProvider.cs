using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator
{
    public class MetricsProvider(Services measuredService = Services.Translator) : MetricProviderBase(measuredService)
    {
    }
}
