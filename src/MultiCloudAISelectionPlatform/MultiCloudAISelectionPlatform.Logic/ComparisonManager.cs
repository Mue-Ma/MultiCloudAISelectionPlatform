using MultiCloudAISelectionPlatform.Common.Enums;
using MultiCloudAISelectionPlatform.Common.Models;
using MultiCloudAISelectionPlatform.Logic.GA;

namespace MultiCloudAISelectionPlatform.Logic
{
    public class ComparisonManager(MetrikWeights metrikWeights)
    {
        private readonly Random _random = new();
        private readonly MetrikWeights _metrikWeights = metrikWeights;

        private double _maxCosts => Convert.ToDouble(_initialComparisonResult?.Max(g => g.Costs) ?? 0);
        private double _minCosts => Convert.ToDouble(_initialComparisonResult?.Min(g => g.Costs) ?? 0);
        private double _maxResponseTime => _initialComparisonResult?.Max(g => g.ResponseTime) ?? 0;
        private double _minResponseTime => _initialComparisonResult?.Min(g => g.ResponseTime) ?? 0;

        private GeneticAlgorithm<ComparisonResult>? _ga;
        private ComparisonResult[]? _initialComparisonResult;
        private ComparisonResult[]? _finalComparisonResults;

        public async Task<ComparisonResult[]> CalculateComparisonResults()
        {
            if (_ga == null || _initialComparisonResult == null) throw new Exception("Metrics or GA not initialized!");

            return await Task.Run(() =>
            {
                while (_ga.BestFitness != 1)
                {
                    Update();
                }
                SetRanks();
                return _finalComparisonResults ?? [];
            });
        }

        public void IntiGA()
        {
            if (_initialComparisonResult == null) throw new Exception("No metrics available, check metrics are initialized before init GA!");

            _ga = new GeneticAlgorithm<ComparisonResult>(
                   (int)Math.Pow(_initialComparisonResult.Length, _initialComparisonResult.Length),
                   _initialComparisonResult.Length, _random, GetRandomCharacter, FitnessFunction, 5);

            _finalComparisonResults = _ga.BestGenes;
        }

        public void IntiMetrics(Services service, Providers[] providers, Guid historicMetricId = default)
        {
            if (historicMetricId != default)
            {
                //TODO load historic metrics from database
            }
            else
            {
                _initialComparisonResult = MetricService.GetMetrics(service, providers)
                    .Select(m => new ComparisonResult
                    {
                        Accuracy = m.DynamicMetrics.Accuracy,
                        Costs = m.StaticMetrics.Costs,
                        Provider = m.Provider,
                        ResponseTime = m.DynamicMetrics.ResponseTime
                    }).ToArray();
            }
        }

        private void UpdateResult(ComparisonResult[] bestGenes) => _finalComparisonResults = bestGenes;

        private void Update()
        {
            if (_ga == null) throw new Exception("No GA instance available, check GA is initialized!");

            _ga.NewGeneration();
            UpdateResult(_ga.BestGenes);
        }

        private ComparisonResult GetRandomCharacter()
        {
            if (_initialComparisonResult == null) throw new Exception("No metrics available, check metrics are initialized before init GA!");

            int i = _random.Next(_initialComparisonResult.Length);
            return _initialComparisonResult[i];
        }

        private double FitnessFunction(int index)
        {
            if (_ga == null || _initialComparisonResult == null) return 0;

            double score = 0;
            DNA<ComparisonResult> dna = _ga.Population[index];

            if (dna.Genes.Any(g => g == null))
            {
                return 0;
            }

            foreach (var gene in dna.Genes)
            {
                if (!dna.Genes.Any(g => IsBetterGene(gene, g) && Array.IndexOf(dna.Genes, g) < Array.IndexOf(dna.Genes, gene)))
                {
                    score += 1;
                }
            }

            score /= _initialComparisonResult.Length;

            return score;
        }

        private void SetRanks()
        {
            if (_finalComparisonResults == null) throw new Exception("No final results available!");

            foreach (var item in _finalComparisonResults)
            {
                item.Rank = Array.IndexOf(_finalComparisonResults, item) + 1;
            }
        }

        private bool IsBetterGene(ComparisonResult gene, ComparisonResult otherGene)
        {
            if (_initialComparisonResult == null) return false;
            if (gene == otherGene) return true;

            double score = 0;

            score += ((1 - GetPercantegeOfIntervall(_minCosts, _maxCosts, (double)gene.Costs))
                - (1 - GetPercantegeOfIntervall(_minCosts, _maxCosts, (double)otherGene.Costs)))
                * _metrikWeights.Costs;

            score += (gene.Accuracy - otherGene.Accuracy) * _metrikWeights.Accuracy;

            score += ((1 - GetPercantegeOfIntervall(_minResponseTime, _maxResponseTime, (double)gene.ResponseTime))
                - (1 - GetPercantegeOfIntervall(_minResponseTime, _maxResponseTime, (double)otherGene.ResponseTime)))
                * _metrikWeights.ResponseTime;

            return score >= 0;
        }

        private static double GetPercantegeOfIntervall(double min, double max, double x)
        {
            return (x - min) / (max + min);
        }
    }
}
