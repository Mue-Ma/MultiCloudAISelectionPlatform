namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator
{
    internal class AWS : MeasureMetricsPerformerBase
    {
        public AWS()
        {
            Provider = Common.Enums.Providers.AWS;
        }

        protected override decimal GetCostsMeasure()
        {
            return 15;
        }
        protected override double GetAccuracyMeasure()
        {
            return 0.9;
        }

        protected override double GetResponseTimeMeasure()
        {
            return 3;
        }
    }
}
