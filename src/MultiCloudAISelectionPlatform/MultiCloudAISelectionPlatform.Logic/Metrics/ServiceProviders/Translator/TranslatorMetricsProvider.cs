using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator
{
    public class TranslatorMetricsProvider(Services measuredService = Services.Translator) : MetricProviderBase(measuredService)
    {
    }
}
