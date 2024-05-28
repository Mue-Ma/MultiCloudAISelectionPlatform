namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator
{
    internal class Azure : MeasureMetricsPerformerBase
    {
        public Azure()
        {
            Provider = Common.Enums.Providers.Azure;
        }

        protected override decimal GetCostsMeasure()
        {
            return 10;
        }

        protected override double GetAccuracyMeasure()
        {
            return 0.7;
        }

        protected override double GetResponseTimeMeasure()
        {
            return 3;
        }
    }
}
