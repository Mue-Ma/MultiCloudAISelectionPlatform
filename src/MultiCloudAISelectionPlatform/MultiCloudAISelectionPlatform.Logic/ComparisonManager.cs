using MultiCloudAISelectionPlatform.Common.Models;

namespace MultiCloudAISelectionPlatform.Logic
{
    public class ComparisonManager(MetrikWeights metrikWeights, ComparisonResult[] metrikResults)
    {
        private readonly MetrikWeights metrikWeights = metrikWeights;
        private readonly ComparisonResult[] metrikResults = metrikResults;

        public async Task<ComparisonResult[]> CalculateComparisonResults()
        {
            throw new NotImplementedException();
        }
    }
}
