public class Entity 
{
    public List<Organella> bodyRoot = new List<Organella>();

    private Entity(List<Organella> bodyRoot)
    {
        this.bodyRoot = bodyRoot;
    }

    // public static Entity Create(TreeNode<Symbol> tree)
    // {
    //     return new Entity(tree.layout(tree));
    // }

    public List<Organella> GetOrganellas()
    {
        List<Organella> result = new List<Organella>();

        for (organ in bodyRoot) {

        }
    }
}

class EntityBuilderVisitor: TreeNodeVisitor<Symbol> 
{
    private List<Organella> result;

    public EntityBuilderVisitor(List<Organella> result) {
        this.result = result;
    }

    public void Visit(Symbol node, Symbol parent) {

    }
}

class SymbolShaper 
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

    Shape makeShapeFor(Symbol symbol) {
        return dictionary[symbol.id];
    }
}