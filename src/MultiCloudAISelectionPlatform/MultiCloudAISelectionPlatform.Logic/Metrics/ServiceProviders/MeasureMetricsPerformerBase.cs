using MultiCloudAISelectionPlatform.Common.Entities;
using MultiCloudAISelectionPlatform.Common.Enums;

namespace MultiCloudAISelectionPlatform.Logic.Metrics.ServiceProviders
{
    internal abstract class MeasureMetricsPerformerBase
    {
        public Providers Provider { get; protected set; }

        public virtual async Task<MetricsResult> PerformMesurement()
        {
            return await Task.Run(() =>
            {
                return new MetricsResult() 
                { 
                    Provider = Provider,
                    StaticMetrics = new StaticMetrics() { Costs = GetCostsMeasure() }
                };
            });
        }

        protected abstract decimal GetCostsMeasure();
    }
}
