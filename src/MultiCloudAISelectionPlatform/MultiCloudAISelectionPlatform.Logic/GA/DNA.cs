using MultiCloudAISelectionPlatform.Common.Models;

namespace MultiCloudAISelectionPlatform.Logic.GA
{
    public class DNA<T> where T : ComparisonResult
    {
        private readonly Random _random;
        private readonly Func<T> _getRandomGene;
        private readonly Func<int, double> _fitnessFunction;

        public T[] Genes { get; private set; }
        public double Fitness { get; private set; }

        public DNA(int size, Random random, Func<T> getRandomGene, Func<int, double> fitnessFunction, bool shouldInitGenes = true)
        {
            Genes = new T[size];
            _random = random;
            _getRandomGene = getRandomGene;
            _fitnessFunction = fitnessFunction;

            if (shouldInitGenes)
            {
                for (int i = 0; i < Genes.Length; i++)
                {
                    var gene = getRandomGene();
                    while (Genes.Any(g => g?.Provider.Equals(gene.Provider) ?? false))
                    {
                        gene = getRandomGene();
                    }
                    Genes[i] = gene;
                }
            }
        }

        public double CalculateFitness(int index)
        {
            Fitness = _fitnessFunction(index);
            return Fitness;
        }

        public DNA<T> Crossover(DNA<T> otherParent)
        {
            DNA<T> child = new(Genes.Length, _random, _getRandomGene, _fitnessFunction, shouldInitGenes: false);

            for (int i = 0; i < Genes.Length; i++)
            {
                SetOrSwapGene(child.Genes, i);
            }

            return child;
        }

        public void Mutate(float mutationRate)
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                if (_random.NextDouble() < mutationRate)
                {
                    SetOrSwapGene(Genes, i);
                }
            }
        }

        /// <summary>
        /// Takes care that every Object can only exist once. 
        /// </summary>
        /// <param name="genes"></param>
        /// <param name="i"></param>
        private void SetOrSwapGene(T[] genes, int i)
        {
            var gene = _getRandomGene();
            var index = Array.FindIndex(genes, g => g?.Provider.Equals(gene.Provider) ?? false);
            if (index != -1)
            {
                if (genes[i] == null)
                {
                    while (genes.Any(g => g?.Provider.Equals(gene.Provider) ?? false))
                    {
                        gene = _getRandomGene();
                    }
                    genes[i] = gene;
                }
                else
                {
                    genes[index] = genes[i];
                    genes[i] = gene;
                }
            }
            genes[i] = gene;
        }
    }
}
