using System;

public class Program
{
	public static void Main()
	{
        Test6();
	}
    
    static void Test6() 
    {
        Console.WriteLine("--------- Test5 ---------");

        Entity entity = GenerateEntity();

        // Entity changedEntity = new GeneticAlgorythm().LayOut(entity, 500, 500);

        Entity changedEntity = new ForceBasedAlgorythm().LayOut(entity, 10);

        PrintEntity(changedEntity);
    }

    public static Entity GenerateEntity() 
    {
        string input = @"
            [seed] * [head][body] 
            [head] - [face][hair]
            [face] * [eyes][nose][mouth]
        ";

        RulesParser parser = new RulesParser();
        List<Production> rules = parser.Parse(input);
        Grammar grammar = new Grammar(rules);

        TreeNode<Symbol> seed = new TreeNode<Symbol>(new Symbol("seed"));
        TreeNode<Symbol> tree = grammar.Expand(seed);

        return Entity.MakeFrom(tree);
    }

    public static void PrintEntity(Entity entity) 
    {
        for (int i = 0; i < entity.GetOrganellas().Count; i++)
        {
            Organella organ = entity.GetOrganellas()[i];
            Console.WriteLine(organ.symbol + " " + organ.shape);
        }

        SymbolMapper mapper = new SymbolMapper(new Dictionary<string, char> {
            { "head", '.' },
            { "body", 'o' },
            { "face", 'f' },
            { "eyes", 'e' },
            { "hair", '~' },
            { "nose", '-' },
            { "mouth", 'm'}
        });
        string picture = AsciiRenderer.Render(entity, mapper);
        Console.WriteLine(picture);

    }
}