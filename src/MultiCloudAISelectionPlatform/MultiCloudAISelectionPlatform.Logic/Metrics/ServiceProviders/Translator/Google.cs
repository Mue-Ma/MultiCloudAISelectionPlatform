using MultiCloudAISelectionPlatform.Common.Entities;

namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator
{
    internal class Google : MeasureMetricsPerformerBase
    {
        public Google()
        {
            Provider = Common.Enums.Providers.Google;
        }

        protected override decimal GetCostsMeasure()
        {
            return 20;
        }

        protected override double GetAccuracyMeasure()
        {
            return 0.99;
        }

        protected override double GetResponseTimeMeasure()
        {
            return 1.5;
        }
    }
}
