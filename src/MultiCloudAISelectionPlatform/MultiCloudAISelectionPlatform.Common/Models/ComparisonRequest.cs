namespace MultiCloudAISelectionPlatform.Common.Models
{
    public class ComparisonRequest
    {
        public string[] Providers { get; set; } = [];
        public string[] Services { get; set; } = [];
        public MetrikWeights MetrikWeights { get; set; } = new();
    }
}
