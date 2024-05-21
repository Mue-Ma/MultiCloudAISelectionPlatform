namespace MultiCloudAISelectionPlatform.Common.Models
{
    public class MetrikWeights
    {
        public int AccuracyWeight { get; set; } = 1;
        public int CostsWeight { get; set; } = 1;
        public int ResponseTime { get; set; } = 1;
    }
}
