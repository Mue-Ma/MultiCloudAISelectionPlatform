using MultiCloudAISelectionPlatform.Common.Entities;

namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator
{
    internal class Azure : MeasureMetricsPerformerBase
    {
        public Azure()
        {
            Provider = Common.Enums.Providers.Azure;
        }

        public override async Task<MetricsResult> PerformMesurement()
        {
            var res = await base.PerformMesurement();

            res.DynamicMetrics.Accuracy = 0.7;
            res.DynamicMetrics.ResponseTime = 4;

            return res;
        }

        protected override decimal GetCostsMeasure()
        {
            return 1;
        }
    }
}
