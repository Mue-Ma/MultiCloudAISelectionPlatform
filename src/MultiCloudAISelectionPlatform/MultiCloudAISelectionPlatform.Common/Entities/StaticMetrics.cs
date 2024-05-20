using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Common.Entities
{
    public class StaticMetrics
    {
        public SupportedProviders Provider { get; set; } = default;
        public decimal Costs { get; set; }
    }
}
