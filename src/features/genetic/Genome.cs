using System;
using System.Text;

struct Gene {
    public string organId;
    public int dx;
    public int dy;
}

public class Genome {

    private static double MUTATION_RATE = 0.1;
    private static double CROSSOVER_RATE = 0.5;

    private static Random random = new Random();
    
    public Entity entity;
    private List<Gene> genes = new List<Gene>();

    public static Genome Randomize(Entity entity) 
    {
        var genes = entity.GetOrganellas()
            .Select(organ => new Gene {
                organId = organ.id,
                dx = random.Next(-10, 10),
                dy = random.Next(-10, 10)
            })
            .ToList();
        return new Genome(entity, genes);
    }

    private Genome(Entity entity, List<Gene> genes)
    {
        this.entity = entity;
        this.genes = genes;
    }

    public Genome[] CrossOver(Genome other)
    {
        //TODO: validate that we can compare them
        List<Gene> childGenes1 = new List<Gene>();
        List<Gene> childGenes2 = new List<Gene>();
        
        for (int i = 0; i < genes.Count; i++)
        {
            var threshold = random.NextDouble() < CROSSOVER_RATE;
            childGenes1.Add( threshold ? genes[i] : other.genes[i]);
            childGenes2.Add( threshold ? other.genes[i] : genes[i]);
        }

        return new Genome[] {new Genome(entity, childGenes1), new Genome(entity, childGenes2) };
    }

    public Genome Mutate()
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

    public double Fitness()
    {
        var organs = entity.GetOrganellas();
        
        // var overlap = distance between parent and child 
        // minus sum of radius of each organ
        var overlap = 
            entity.GetPairs()
                .Select(p => new { shape1 = p.Item1.shape, shape2 = p.Item2.shape })
                .Select(p => new { 
                    shape1 = p.shape1, 
                    shape2 = p.shape2, 
                    overlap = p.shape1.Overlap(p.shape2)
                })
                .Sum(p => p.overlap);

        // var closeness = distance between each organ and its parent minus sum of radiuses
        var e = this.entity;

        var closeness = 
            organs
                .Where(p => e.GetParent(p) != null)
                .Select(p => new { 
                    shape = p.shape, 
                    parentShape = e.GetParent(p)!.shape 
                })
                .Select(p => new { 
                    shape = p.shape, 
                    parentShape = p.parentShape, 
                    overlap = p.shape.Overlap(p.parentShape)
                })
                .Sum(p => p.overlap);

        var rootDistance = organs
            .Select(p => p.shape.RootDistanceSquared())
            .Sum();

        return overlap + rootDistance;
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

    public override string ToString()
    {
        return genes
            .Select(g => $">{entity.GetById(g.organId)} {g.dx} {g.dy}\n")
            .Aggregate((a, b) => $"{a}{b}");
    }

    public Entity ToEntity() 
    {
        var organs = entity.GetOrganellas();
        var newOrgans = new List<Organella>();
        for (int i = 0; i < genes.Count; i++)
        {
            var gene = genes[i];
            var organ = organs[i];
            var shape = new Shape(organ.shape.centerX + gene.dx, organ.shape.centerY + gene.dy, organ.shape.radius);
            newOrgans.Add(new Organella(organ.symbol, shape, organ.id));
        }
        return entity.MakeWithOrgans(newOrgans);
    }
}