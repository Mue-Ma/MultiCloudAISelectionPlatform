using MultiCloudAISelectionPlatform.Common.Models;

namespace MultiCloudAISelectionPlatform.Logic
{
    public class ResultService
    {
        public static async Task<ComparisonResult[]> GetResults(ComparisonRequest request)
        { 
            return await Task.Run(() => 
            {
                return (new List<ComparisonResult>
                {
                    new() 
                    {
                        Rank = 1,
                        Accuracy = 123.42,
                        Costs = 20,
                        Provider = "Azure",
                        ResponseTime = 3.2
                    },
                    new() 
                    {
                        Rank = 2,
                        Accuracy = 123.42,
                        Costs = 20,
                        Provider = "Google",
                        ResponseTime = 3.2
                    },
                    new() 
                    {
                        Rank = 3,
                        Accuracy = 123.42,
                        Costs = 20,
                        Provider = "AWS",
                        ResponseTime = 3.2
                    }
                }).ToArray();
            
            });
        }
    }
}
