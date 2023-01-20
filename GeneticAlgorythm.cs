public class GeneticAlgorythm 
{
    // Combine with the one in Genome
    private static Random random = new Random();

    public Entity LayOut(Entity entity, int populationSize, int iterations) {
        var population = new List<Genome>();
        for (int i = 0; i < populationSize; i++) {
            population.Add(Genome.Randomize(entity));
        }
        Console.WriteLine("Random entity: " + population.First());

        for (int i = 0; i < iterations; i++) {
            population = population
                .Select(genome => genome.Mutate())
                .OrderBy(genome => genome.Fitness())
                .Take(populationSize / 2)
                .Select(genome => genome.CrossOver(population[random.Next(0, populationSize / 2)]))
                .SelectMany(x => x)
                .ToList();
        }

        var theBest = population
            .OrderBy(genome => genome.Fitness())
            .First();

        return theBest.ToEntity();
    }
} 