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

        Visit(tree, null, parents, organs);

        return new Entity(parents, organs);
    }

    private static void Visit(
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
            Visit(child, organ, parents, organs);
        }
    }

    public List<Organella> GetOrganellas()
    {
        return organs.Values.ToList();
    }
}


public class SymbolShaper 
{
    private Dictionary<string, Shape> dictionary = new Dictionary<string, Shape>() {
            {"seed",  new Shape(0.0, 0.0, 0) },
            {"head",  new Shape(0.0, 0.0, 5) },
            {"body",  new Shape(0.0, 0.0, 40) },
            {"face",  new Shape(0.0, 0.0, 4) },
            {"eyes",  new Shape(0.0, 0.0, 1) },
            {"hair",  new Shape(0.0, 0.0, 1) },
            {"nose",  new Shape(0.0, 0.0, 1) },
            {"mouth",  new Shape(0.0, 0.0, 3) },
    };

    public Shape makeShapeFor(Symbol symbol) {
        return dictionary[symbol.id];
    }
}