using MultiCloudAISelectionPlatform.Common.Entities;

namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator
{
    internal class AWS : MeasureMetricsPerformerBase
    {
        public AWS()
        {
            Provider = Common.Enums.Providers.AWS;
        }

        public override async Task<MetricsResult> PerformMesurement()
        {
            var res = await base.PerformMesurement();

            res.DynamicMetrics.Accuracy = 0.9;
            res.DynamicMetrics.ResponseTime = 3;

            return res;
        }

        protected override decimal GetCostsMeasure()
        {
            return 1;
        }
    }
}
