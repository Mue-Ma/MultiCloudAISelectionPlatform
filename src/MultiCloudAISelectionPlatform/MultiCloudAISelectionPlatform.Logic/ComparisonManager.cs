using MultiCloudAISelectionPlatform.Common.Enums;
using MultiCloudAISelectionPlatform.Common.Models;
using MultiCloudAISelectionPlatform.Logic.GA;

namespace MultiCloudAISelectionPlatform.Logic
{
    public class ComparisonManager
    {
        private readonly Random _random;
        private GeneticAlgorithm<ComparisonResult>? _ga;
        private ComparisonResult[]? _initialComparisonResult;
        private ComparisonResult[]? _finalComparisonResults;

        public ComparisonManager()
        {
            _random = new Random();
        }

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
            if(_ga == null) throw new Exception("No GA instance available, check GA is initialized!");

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
                if (!dna.Genes.Any(g => g.MeasureMetrik > gene.MeasureMetrik && Array.IndexOf(dna.Genes, g) < Array.IndexOf(dna.Genes, gene)))
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
    }
}
