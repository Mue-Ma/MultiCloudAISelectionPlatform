namespace MultiCloudAISelectionPlatform.Common.Models
{
    public class ComparisonResult
    {
        public double Accuracy { get; set; }
        public decimal Costs { get; set; }
        public double ResponseTime { get; set; }
        public string Provider { get; set; } = string.Empty;
        public int Rank { get; set; }
    }
}
