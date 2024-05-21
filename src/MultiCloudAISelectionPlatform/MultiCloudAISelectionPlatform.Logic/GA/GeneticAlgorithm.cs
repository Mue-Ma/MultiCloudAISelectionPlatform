using MultiCloudAISelectionPlatform.Common.Models;

namespace MultiCloudAISelectionPlatform.Logic.GA
{
    public class GeneticAlgorithm<T> where T : ComparisonResult
    {
        private readonly int _dnaSize;
        private readonly Random _random;
        private readonly Func<T> _getRandomGene;
        private readonly Func<int, double> _fitnessFunction;
        private double fitnessSum;
        private List<DNA<T>> newPopulation;

        public int Elitism { get; private set; }
        public float MutationRate { get; private set; }
        public int Generation { get; private set; }
        public double BestFitness { get; private set; }
        public T[] BestGenes { get; private set; }
        public List<DNA<T>> Population { get; private set; }

        public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<T> getRandomGene, Func<int, double> fitnessFunction,
            int elitism, float mutationRate = 0.01f)
        {
            _random = random;
            _dnaSize = dnaSize;
            _getRandomGene = getRandomGene;
            _fitnessFunction = fitnessFunction;

            Generation = 1;
            Elitism = elitism;
            MutationRate = mutationRate;
            Population = new List<DNA<T>>(populationSize);
            newPopulation = new List<DNA<T>>(populationSize);
            BestGenes = new T[dnaSize];

            for (int i = 0; i < populationSize; i++)
            {
                Population.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
            }
        }

        public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
        {
            int finalCount = Population.Count + numNewDNA;

            if (finalCount <= 0)
            {
                return;
            }

            if (Population.Count > 0)
            {
                CalculateFitness();
                Population.Sort(CompareDNA);
            }
            newPopulation.Clear();

            for (int i = 0; i < Population.Count; i++)
            {
                if (i < Elitism && i < Population.Count)
                {
                    newPopulation.Add(Population[i]);
                }
                else if (i < Population.Count || crossoverNewDNA)
                {
                    DNA<T> parent1 = ChooseParent();
                    DNA<T> parent2 = ChooseParent();

                    DNA<T> child = parent1.Crossover(parent2);

                    child.Mutate(MutationRate);

                    newPopulation.Add(child);
                }
                else
                {
                    newPopulation.Add(new DNA<T>(_dnaSize, _random, _getRandomGene, _fitnessFunction, shouldInitGenes: true));
                }
            }

            List<DNA<T>> tmpList = Population;
            Population = newPopulation;
            newPopulation = tmpList;

            Generation++;
        }

        private int CompareDNA(DNA<T> a, DNA<T> b)
        {
            if (a.Fitness > b.Fitness)
            {
                return -1;
            }
            else if (a.Fitness < b.Fitness)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void CalculateFitness()
        {
            fitnessSum = 0;
            DNA<T> best = Population[0];

            for (int i = 0; i < Population.Count; i++)
            {
                fitnessSum += Population[i].CalculateFitness(i);

                if (Population[i].Fitness > best.Fitness)
                {
                    best = Population[i];
                }
            }

            BestFitness = best.Fitness;
            best.Genes.CopyTo(BestGenes, 0);
        }

        private DNA<T> ChooseParent()
        {
            double randomNumber = _random.NextDouble() * fitnessSum;

            for (int i = 0; i < Population.Count; i++)
            {
                if (randomNumber < Population[i].Fitness)
                {
                    return Population[i];
                }

                randomNumber -= Population[i].Fitness;
            }

            return null;
        }
    }
}
