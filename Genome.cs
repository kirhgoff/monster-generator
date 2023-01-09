struct Gene {
    public string shapeId;
    public int dx;
    public int dy;
}

public class Genome {
    
    private Dictionary<string, Shape> shapes;
    private List<Gene> genes = new List<Gene>();
    public Genome(List<Shape> shapes) 
    {
        this.shapes = shapes.ToDictionary(s => s.id, s => s);
        this.genes = shapes
            .Select(s => new Gene { shapeId = s.id, dx = 0, dy = 0 })
            .ToList();
    }

    public Genome crossOver(Genome other)
    {
        //validate
        List<Gene> childGenes = new List<Gene>();
        
        for (int i = 0; i < genes.Count; i++)
        {
            if (Random.value < 0.5)
            {
                child.genes[i] = genes[i];
            }
            else
            {
                child.genes[i] = other.genes[i];
            }
        }
        return child;
    }
}