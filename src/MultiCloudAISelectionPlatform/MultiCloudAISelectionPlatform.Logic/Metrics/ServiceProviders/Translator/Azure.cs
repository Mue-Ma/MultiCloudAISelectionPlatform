using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator
{
    internal class Azure(Providers provider = Providers.Azure) : MeasureMetricsPerformerBase(provider)
    {
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
