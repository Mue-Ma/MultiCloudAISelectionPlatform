using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator
{
    internal class Google(Providers provider = Providers.Google) : MeasureMetricsPerformerBase(provider)
    {
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
