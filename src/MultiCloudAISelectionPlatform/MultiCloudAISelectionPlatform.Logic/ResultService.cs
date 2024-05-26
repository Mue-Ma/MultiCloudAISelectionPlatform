using MultiCloudAISelectionPlatform.Common.Models;

namespace MultiCloudAISelectionPlatform.Logic
{
    public class ResultService
    {
        public async Task<ComparisonResult[]> GetResults(ComparisonRequest request)
        {
            ComparisonManager comparisonManager = new();

            comparisonManager.IntiMetrics(request.Service, request.Providers);
            comparisonManager.IntiGA();

            return await comparisonManager.CalculateComparisonResults();
        }
    }
}
