using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Common.Models
{
    public class ComparisonResult
    {
        public double Accuracy { get; set; }
        public decimal Costs { get; set; }
        public double ResponseTime { get; set; }
        public Providers Provider { get; set; } = default;
        public int Rank { get; set; }
    }
}
