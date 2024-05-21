using MultiCloudAISelectionPlatform.Common.Models;

namespace MultiCloudAISelectionPlatform.Logic
{
    public class ResultService
    {
        public async Task<ComparisonResult[]> GetResults(ComparisonRequest request)
        {
            ComparisonManager comparisonManager = new();
            return await comparisonManager.CalculateComparisonResults(request);
        }
    }
}
