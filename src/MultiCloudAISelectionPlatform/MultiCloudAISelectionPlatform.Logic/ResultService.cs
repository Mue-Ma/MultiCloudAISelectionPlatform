using MultiCloudAISelectionPlatform.Common.Models;
using MultiCloudAISelectionPlatform.Logic.GA;

namespace MultiCloudAISelectionPlatform.Logic
{
    public class ResultService
    {
        ComparisonResult[] FirstComparisonResult =>
        [
                new()
                {
                    Rank = 1,
                    Accuracy = 123.42,
                    Costs = 20,
                    Provider = "Azure",
                    ResponseTime = 3.2
                },
            new()
            {
                Rank = 2,
                Accuracy = 123.42,
                Costs = 20,
                Provider = "Google",
                ResponseTime = 3.2
            },
            new()
            {
                Rank = 3,
                Accuracy = 123.42,
                Costs = 20,
                Provider = "AWS",
                ResponseTime = 3.2
            }
        ];

        ComparisonResult[] ComparisonResults;

        private readonly GeneticAlgorithm<ComparisonResult> ga;
        private readonly Random random;

        public ResultService()
        {
            random = new Random();
            ga = new GeneticAlgorithm<ComparisonResult>(200, 3, random, GetRandomCharacter, FitnessFunction, 5);
            ComparisonResults = ga.BestGenes;
        }

        public async Task<ComparisonResult[]> GetResults(ComparisonRequest request)
        {
            return await Task.Run(() =>
            {
                while (ga.BestFitness != 1)
                {
                    Update();
                }

                return ComparisonResults;
            });
        }

        void Update()
        {
            ga.NewGeneration();
            UpdateResult(ga.BestGenes);
        }

        private void UpdateResult(ComparisonResult[] bestGenes)
        {
            ComparisonResults = bestGenes;
        }

        private ComparisonResult GetRandomCharacter()
        {
            int i = random.Next(FirstComparisonResult.Length);
            return FirstComparisonResult[i];
        }

        private double FitnessFunction(int index)
        {
            if (ga == null) return 0;

            double score = 0;
            DNA<ComparisonResult> dna = ga.Population[index];

            foreach (var gene in dna.Genes)
            {
                if (!dna.Genes.Any(g => g.MeasureMetrik > gene.MeasureMetrik && g.Rank < gene.Rank))
                {
                    score += 1;
                }
            }

            score /= FirstComparisonResult.Length;

            score = (Math.Pow(2, score) - 1) / (2 - 1);

            return score;
        }
    }
}
