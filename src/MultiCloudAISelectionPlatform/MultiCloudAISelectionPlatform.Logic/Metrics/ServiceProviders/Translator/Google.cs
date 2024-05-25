using MultiCloudAISelectionPlatform.Common.Entities;

namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator
{
    internal class Google : MeasureMetricsPerformerBase
    {
        public Google()
        {
            Provider = Common.Enums.Providers.AWS;
        }

        public override async Task<MetricsResult> PerformMesurement()
        {
            var res = await base.PerformMesurement();

            res.DynamicMetrics.Accuracy = 0.99;
            res.DynamicMetrics.ResponseTime = 1.5;
            res.StaticMetrics.Costs = 40;

            return res;
        }
    }
}
