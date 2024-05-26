using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Common.Models
{
    public class ComparisonRequest
    {
        public Providers[] Providers { get; set; } = [];
        public Services Service { get; set; } = default;
        public MetrikWeights MetrikWeights { get; set; } = new();
    }
}
