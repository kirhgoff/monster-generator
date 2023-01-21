using System;

public class Program
{
	public static void Main()
	{
        // Test1();
        // Test2();
        // Test3();
        // Test4();
        Test5();
	}
    
    static void Test5() 
    {
        Console.WriteLine("--------- Test5 ---------");

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

        Entity entity = Entity.MakeFrom(tree);

        Console.WriteLine(">>> Pairs <<<");
        foreach ((Organella child, Organella parent) in entity.GetPairs())
        {
            Console.WriteLine(child + " => " + parent);
        }

        Entity changedEntity = new GeneticAlgorythm().LayOut(entity, 500, 500);

        SymbolMapper mapper = new SymbolMapper(new Dictionary<string, char> {
            { "head", '.' },
            { "body", 'o' },
            { "face", 'f' },
            { "eyes", 'e' },
            { "hair", '~' },
            { "nose", '-' },
            { "mouth", 'm'}
        });
        string picture = AsciiRenderer.Render(changedEntity, mapper);
        Console.WriteLine(picture);
    }
}