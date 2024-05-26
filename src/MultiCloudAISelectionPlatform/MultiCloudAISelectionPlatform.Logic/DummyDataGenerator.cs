using MultiCloudAISelectionPlatform.Common.Enums;
using MultiCloudAISelectionPlatform.Common.Models;

namespace MultiCloudAISelectionPlatform.Logic
{
    public static class DummyDataGenerator
    {
        public static List<ComparisonResult> GenerateDummyComparisonResults()
        {
            var results = new List<ComparisonResult>
            {
                new ComparisonResult { Accuracy = 0.95, Costs = 10, ResponseTime = 1.2, Provider = Providers.Azure, Rank = 1 },
                new ComparisonResult { Accuracy = 0.90, Costs = 15, ResponseTime = 1.5, Provider = Providers.Google, Rank = 2 },
                new ComparisonResult { Accuracy = 0.85, Costs = 20, ResponseTime = 1.1, Provider = Providers.AWS, Rank = 3 }
            };

            return results;
        }

        public static ComparisonRequest GenerateDummyComparisonRequest()
        {
            return new ComparisonRequest
            {
                MetrikWeights = new MetrikWeights { AccuracyWeight = 1, CostsWeight = 1, ResponseTime = 1 }
            };
        }
    }
}