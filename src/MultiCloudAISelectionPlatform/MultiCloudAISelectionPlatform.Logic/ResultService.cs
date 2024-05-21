using Microsoft.VisualBasic;
using MultiCloudAISelectionPlatform.Common.Models;
using MultiCloudAISelectionPlatform.Logic.GA;

namespace MultiCloudAISelectionPlatform.Logic
{
    public class ResultService
    {
        ComparisonResult[] InitialComparisonResult;
        ComparisonResult[] FinalComparisonResults;

        private readonly GeneticAlgorithm<ComparisonResult> ga;
        private readonly Random random;

        public ResultService()
        {
            InitialComparisonResult = MetricService.GetMetrics();
            random = new Random();
            ga = new GeneticAlgorithm<ComparisonResult>(20, 3, random, GetRandomCharacter, FitnessFunction, 5);
            FinalComparisonResults = ga.BestGenes;

        }

        public async Task<ComparisonResult[]> GetResults(ComparisonRequest request)
        {
            return await Task.Run(() =>
            {
                while (ga.BestFitness != 1)
                {
                    Update();
                }
                SetRanks();
                return FinalComparisonResults;
            });
        }

        void Update()
        {
            ga.NewGeneration();
            UpdateResult(ga.BestGenes);
        }

        private void UpdateResult(ComparisonResult[] bestGenes)
        {
            FinalComparisonResults = bestGenes;
        }

        private ComparisonResult GetRandomCharacter()
        {
            int i = random.Next(InitialComparisonResult.Length);
            return InitialComparisonResult[i];
        }

        private double FitnessFunction(int index)
        {
            if (ga == null) return 0;

            double score = 0;
            DNA<ComparisonResult> dna = ga.Population[index];

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

            score /= InitialComparisonResult.Length;

            return score;
        }

        private void SetRanks()
        {
            foreach (var item in FinalComparisonResults)
            {
                item.Rank = Array.IndexOf(FinalComparisonResults, item) + 1;
            }
        }
    }
}
