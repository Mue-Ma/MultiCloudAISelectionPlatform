using MultiCloudAISelectionPlatform.Common.Models;

namespace MultiCloudAISelectionPlatform.Logic.GA
{
    public class DNA<T> where T : ComparisonResult
    {
        public T[] Genes { get; private set; }
        public double Fitness { get; private set; }

        private readonly Random random;
        private readonly Func<T> getRandomGene;
        private readonly Func<int, double> fitnessFunction;

        public DNA(int size, Random random, Func<T> getRandomGene, Func<int, double> fitnessFunction, bool shouldInitGenes = true)
        {
            Genes = new T[size];
            this.random = random;
            this.getRandomGene = getRandomGene;
            this.fitnessFunction = fitnessFunction;

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
            Fitness = fitnessFunction(index);
            return Fitness;
        }

        public DNA<T> Crossover(DNA<T> otherParent)
        {
            DNA<T> child = new(Genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

            for (int i = 0; i < Genes.Length; i++)
            {
                var gene = random.NextDouble() < 0.5 ? Genes[i] : otherParent.Genes[i];
                var index = Array.FindIndex(child.Genes, g => g?.Provider.Equals(gene.Provider) ?? false);
                if (index != -1)
                {
                    if (Genes[i] == null)
                    {
                        while (Genes.Any(g => g?.Provider.Equals(gene.Provider) ?? false))
                        {
                            gene = getRandomGene();
                        }
                        child.Genes[i] = gene;
                    }
                    else
                    {
                        child.Genes[index] = child.Genes[i];
                        child.Genes[i] = gene;
                    }
                }
                child.Genes[i] = gene;
            }

            return child;
        }

        public void Mutate(float mutationRate)
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    var gene = getRandomGene();
                    var index = Array.FindIndex(Genes, g => g?.Provider.Equals(gene.Provider) ?? false);
                    if (index != -1)
                    {
                        if (Genes[i] == null)
                        {
                            while (Genes.Any(g => g?.Provider.Equals(gene.Provider) ?? false))
                            {
                                gene = getRandomGene();
                            }
                            Genes[i] = gene;
                        }
                        else 
                        {
                            Genes[index] = Genes[i];
                            Genes[i] = gene;
                        }
                    }
                    Genes[i] = gene;
                }
            }
        }
    }
}
