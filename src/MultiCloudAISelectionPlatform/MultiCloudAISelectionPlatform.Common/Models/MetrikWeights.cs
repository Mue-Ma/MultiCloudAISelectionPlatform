namespace MultiCloudAISelectionPlatform.Common.Models
{
    public class MetrikWeights
    {
        public int Accuracy { get; set; } = 1;
        public int Costs { get; set; } = 1;
        public int ResponseTime { get; set; } = 1;
    }
}
