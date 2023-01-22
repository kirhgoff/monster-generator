public class Entity 
{
    // organella id
    private Dictionary<string, string?> parents;
    private Dictionary<string, Organella> organs;

    private Entity(Dictionary<string, string?> parents, Dictionary<string, Organella> organs)
    {
        this.parents = parents;
        this.organs = organs;
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

    public List<Organella> GetRoots() 
    {
        return parents
            .Select(pair => pair.Key)
            .Where(id => parents[id] == null)
            .Select(id => organs[id])
            .ToList();
    }

    public List<Organella> GetChildren(Organella organ)
    {
        return parents
            .Where(pair => pair.Value == organ.id)
            .Select(pair => organs[pair.Key])
            .ToList();
    }

    public List<(Organella, Organella)> GetPairs()
    {
        return parents
            .Where(pair => pair.Value != null)
            .Select(pair => (organs[pair.Value!], organs[pair.Key]))
            .ToList();
    }

    public List<Organella> GetOrganellas()
    {
        return organs.Values.ToList();
    }

    public Entity MakeCopy()
    {
        return MakeWithOrgans(GetOrganellas());
    }

    public Organella? GetParent(Organella organ)
    {
        if (!parents.ContainsKey(organ.id))
        {
            return null;
        } 

        return organs[parents[organ.id]!];
    }

    public Organella? GetById(string organId)
    {
        if (!organs.ContainsKey(organId))
        {
            return null;
        }
        
        return organs[organId];
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