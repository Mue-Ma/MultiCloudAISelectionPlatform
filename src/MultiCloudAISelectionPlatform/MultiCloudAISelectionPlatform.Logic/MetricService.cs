using MultiCloudAISelectionPlatform.Common.Entities;
using MultiCloudAISelectionPlatform.Common.Models;

namespace MultiCloudAISelectionPlatform.Logic
{
    public static class MetricService
    {
        public static StaticMetrics[] GetStaticMetrics()
        {
            throw new NotImplementedException();
        }

        public static DynamicMetric[] GetDynamicMetrics()
        {
            throw new NotImplementedException();
        }

        internal static ComparisonResult[] GetMetrics()
        {
            return 
            [
                new()
                {
                    Accuracy = 0.7,
                    Costs = 15,
                    Provider = Common.Enums.SupportedProviders.Azure,
                    ResponseTime = 4
                },
                new()
                {
                    Accuracy = 0.9,
                    Costs = 1,
                    Provider = Common.Enums.SupportedProviders.Google,
                    ResponseTime = 3.2
                },
                new()
                {
                    Accuracy = 0.5,
                    Costs = 20,
                    Provider = Common.Enums.SupportedProviders.AWS,
                    ResponseTime = 2
                }
            ];
        }
    }
}
