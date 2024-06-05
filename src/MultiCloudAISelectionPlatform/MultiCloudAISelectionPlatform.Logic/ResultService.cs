using MultiCloudAISelectionPlatform.Common.Models;

namespace MultiCloudAISelectionPlatform.Logic
{
    public class ResultService
    {
        public async Task<ComparisonResult[]> GetResults(ComparisonRequest request)
        {
            using ComparisonManager comparisonManager = new(request.MetrikWeights);
            comparisonManager.IntiMetrics(request.Service, request.Providers);
            comparisonManager.IntiGA();
            return await comparisonManager.CalculateComparisonResults();
        }
    }
}
