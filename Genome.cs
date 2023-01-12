struct Gene {
    public string organId;
    public int dx;
    public int dy;
}

public class Genome {

    private static double MUTATION_RATE = 0.1;
    private static double CROSSOVER_RATE = 0.5;

    private static Random random = new Random();
    
    private Entity entity;
    private List<Gene> genes = new List<Gene>();

    public Genome(Entity entity) 
    {
        this.entity = entity;
        this.genes = entity
            .GetOrganellas()
            .Select(o => new Gene { organId = o.id, dx = 0, dy = 0 })
            .ToList();
    }

    private Genome(Entity entity, List<Gene> genes)
    {
        this.entity = entity;
        this.genes = genes;
    }

    public Genome crossOver(Genome other)
    {
        //TODO: validate that we can compare them
        List<Gene> childGenes = new List<Gene>();
        
        for (int i = 0; i < genes.Count; i++)
        {
            childGenes.Add(random.NextDouble() < CROSSOVER_RATE ? genes[i] : other.genes[i]);
        }

        return new Genome(entity, childGenes);
    }

    public Genome mutate()
    {
        List<Gene> mutatedGenes = new List<Gene>();

        foreach (var gene in genes)
        {
            if (random.NextDouble() < MUTATION_RATE)
            {
                mutatedGenes.Add(new Gene
                {
                    organId = gene.organId,
                    dx = gene.dx + random.Next(-1, 1),
                    dy = gene.dy + random.Next(-1, 1)
                });
            }
            else
            {
                mutatedGenes.Add(gene);
            }
        }

        return new Genome(entity, mutatedGenes);
    }

    public double fitness()
    {
        //TODO: working here
        return 0;

        // var overlap = GetPermutations(this.shapes.Values, 2)
        //     .Select(p => p.ToArray())
        //     .Select(p => new { shape1 = p[0], shape2 = p[1] })
        //     .Select(p => new { 
        //         shape1 = p.shape1, 
        //         shape2 = p.shape2, 
        //         overlap = p.shape1.overlapSquared(p.shape2)
        //     })
        //     .Sum(p => p.overlap);

        // var rootDistance = this.shapes
        //     .Select(p => p.Value.rootDistanceSquared())
        //     .Sum();

        // return overlap + rootDistance;
    }

    IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
    {
        int i = 0;
        foreach(var item in items)
        {
            if(count == 1)
                yield return new T[] { item };
            else
            {
                foreach(var result in GetPermutations(items.Skip(i + 1), count - 1))
                    yield return new T[] { item }.Concat(result);
            }

            ++i;
        }
    }
}