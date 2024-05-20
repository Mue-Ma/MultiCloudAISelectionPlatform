using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Common.Entities
{
    public class DynamicMetric
    {
        public double Accuracy { get; set; }
        public decimal Costs { get; set; }
        public double ResponseTime { get; set; }
        public SupportedProviders Provider { get; set; } = default;
    }
}
