public class Entity 
{
    // organella id
    private Dictionary<string, string?> parents;
    private Dictionary<string, Organella> organs;

    private Entity(Dictionary<string, string?> parents, Dictionary<string, Organella> organs)
    {
        this.parents = parents;
        this.organs = organs;

        Console.WriteLine(">>> Parents <<<");
        foreach (var pair in parents)
        {
            Console.WriteLine($"{pair.Key} => {pair.Value}");
        }
    }

    public static Entity MakeFrom(TreeNode<Symbol> tree)
    {
        Dictionary<string, string?> parents = new Dictionary<string, string?>();
        Dictionary<string, Organella> organs = new Dictionary<string, Organella>();

        Process(tree, null, parents, organs);

        return new Entity(parents, organs);
    }

    public Entity MakeWithOrgans(List<Organella> organs)
    {
        return new Entity(parents, organs.ToDictionary(o => o.id));
    }

    private static void Process(
        TreeNode<Symbol> node, 
        Organella? parent,
        Dictionary<string, string?> parents,
        Dictionary<string, Organella> organs
    ) 
    {
        var shape = new SymbolShaper().makeShapeFor(node.data);
        var organ = new Organella(node.data, shape);
        organs.Add(organ.id, organ);
        if (parent != null)
        {
            parents.Add(organ.id, parent.id);
        }
        foreach (var child in node.children)
        {
            Process(child, organ, parents, organs);
        }
    }

    public Organella getById(string organId)
    {
        return organs[organId];
    }

    public List<Organella> getRoots() 
    {
        return parents
            .Select(pair => pair.Key)
            .Where(id => parents[id] == null)
            .Select(id => organs[id])
            .ToList();
    }

    public List<Organella> getChildren(Organella organ)
    {
        return parents
            .Where(pair => parents[pair.Key] == organ.id)
            .Select(pair => pair.Value)
            .Where(id => id != null)
            .Select(id => organs[id])
            .Where(organ => organ != null)
            .ToList();
    }

    public List<Organella> GetOrganellas()
    {
        return organs.Values.ToList();
    }

    public Organella? GetParent(Organella organ)
    {
        if (parents.ContainsKey(organ.id) == false)
        {
            return null;
        } 
        else 
        {            
            return organs[parents[organ.id]!];
        }
    }
}


public class SymbolShaper 
{
    private Dictionary<string, Shape> dictionary = new Dictionary<string, Shape>() {
            {"seed",  new Shape(0.0, 0.0, 0) },
            {"head",  new Shape(0.0, 0.0, 5) },
            {"body",  new Shape(0.0, 0.0, 7) },
            {"face",  new Shape(0.0, 0.0, 4) },
            {"eyes",  new Shape(0.0, 0.0, 1) },
            {"hair",  new Shape(0.0, 0.0, 1) },
            {"nose",  new Shape(0.0, 0.0, 1) },
            {"mouth",  new Shape(0.0, 0.0, 3) },
    };

    public Shape makeShapeFor(Symbol symbol) {
        return dictionary[symbol.id].Copy();
    }
}