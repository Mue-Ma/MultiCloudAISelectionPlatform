using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Common.Entities
{
    public class MetricsResult
    {
        public DateTime PerformanceDate { get; set; } = DateTime.Now;
        public DynamicMetrics DynamicMetrics { get; set; } = new();
        public StaticMetrics StaticMetrics { get; set; } = new();
        public Providers Provider { get; set; } = default;
        public Services Service { get; set; } = default;
    }
}
