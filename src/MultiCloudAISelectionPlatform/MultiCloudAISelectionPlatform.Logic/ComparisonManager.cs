using MultiCloudAISelectionPlatform.Common.Models;
using MultiCloudAISelectionPlatform.Logic.GA;

namespace MultiCloudAISelectionPlatform.Logic
{
    public class ComparisonManager
    {
        private readonly Random _random;
        private readonly GeneticAlgorithm<ComparisonResult> _ga;
        private readonly ComparisonResult[] _initialComparisonResult;
        private ComparisonResult[] _finalComparisonResults;

        public ComparisonManager()
        {
            _random = new Random();

            _initialComparisonResult = MetricService.GetTranslatorMetrics().Select(m => new ComparisonResult 
            {
                Accuracy = m.DynamicMetrics.Accuracy,
                Costs = m.StaticMetrics.Costs,
                Provider = m.Provider,
                ResponseTime = m.DynamicMetrics.ResponseTime
            }).ToArray();

            _ga = new GeneticAlgorithm<ComparisonResult>(20, 3, _random, GetRandomCharacter, FitnessFunction, 5);
            _finalComparisonResults = _ga.BestGenes;
        }

        public async Task<ComparisonResult[]> CalculateComparisonResults(ComparisonRequest request)
        {
            return await Task.Run(() =>
            {
                while (_ga.BestFitness != 1)
                {
                    Update();
                }
                SetRanks();
                return _finalComparisonResults;
            });
        }

        private void UpdateResult(ComparisonResult[] bestGenes) => _finalComparisonResults = bestGenes;

        private void Update()
        {
            _ga.NewGeneration();
            UpdateResult(_ga.BestGenes);
        }

        private ComparisonResult GetRandomCharacter()
        {
            int i = _random.Next(_initialComparisonResult.Length);
            return _initialComparisonResult[i];
        }

        private double FitnessFunction(int index)
        {
            if (_ga == null) return 0;

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
            foreach (var item in _finalComparisonResults)
            {
                item.Rank = Array.IndexOf(_finalComparisonResults, item) + 1;
            }
        }
    }
}
