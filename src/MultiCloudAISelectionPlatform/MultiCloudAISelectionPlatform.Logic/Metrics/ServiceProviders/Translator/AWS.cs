using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders.Translator
{
    internal class AWS(Providers provider = Providers.AWS) : MeasureMetricsPerformerBase(provider)
    {
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
