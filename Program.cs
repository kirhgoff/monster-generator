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

    static void Test1() 
    {
        Console.WriteLine("--------- Test1 ---------");

		Symbol input = new Symbol("seed");
		List<Symbol> output = new List<Symbol>{ new Symbol("head"), new Symbol("body") };
		Production prod1 = new Production(RuleType.Expanding, input, output);

		Console.WriteLine(prod1.ToString());        
    }
    static void Test2()
    {
        Console.WriteLine("--------- Test2 ---------");

        string input = @"
            [seed] * [head][body] 
            [head] - [face][hair]
            [body] - [torso][legs]
            [face] - [eyes][nose][mouth]
            [torso] - [chest][hands]
            [legs] * [leg][leg]
            [hands] * [hand][hand]
        ";

        RulesParser parser = new RulesParser();
        List<Production> rules = parser.Parse(input);

        foreach (Production rule in rules)
        {
            Console.WriteLine(rule.ToString());
        }
    }

    static void Test3() 
    {
        Console.WriteLine("--------- Test3 ---------");

        string input = @"
            [seed] * [head][body] 
            [head] - [face][hair]
            [face] * [eyes][nose][mouth]
        ";

        string chars = @"
            seed => s
            head => h
            body => b
            face => f
            eyes => e
            hair => ~
            nose => -
            mouth => m
        ";

        RulesParser parser = new RulesParser();
        List<Production> rules = parser.Parse(input);

        Grammar grammar = new Grammar(rules);

        TreeNode<Symbol> seed = new TreeNode<Symbol>(new Symbol("seed"));
        TreeNode<Symbol> tree = grammar.Expand(seed);

        Console.WriteLine(">>> Tree <<<");
        new TreeNodePrinter(chars).Print(tree);
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

        Console.WriteLine(">>> Organellas <<<");
        foreach (Organella organella in entity.GetOrganellas())
        {
            Organella? parent = entity.GetParent(organella);
            var parentName = (parent == null) ? "null" : parent.symbol.ToString();
            Console.WriteLine(organella.ToString() + " => " + parentName);
        }

        Genome genome = new Genome(entity);
        Console.WriteLine(">>> Genome 1 <<<");
        Console.WriteLine(genome.ToString());

        Console.WriteLine(">>> Genome 2 <<<");
        for (int i = 0; i < 100; i++)
        {
            genome = genome.mutate();
        }
        Console.WriteLine(genome.ToString());

        var changedEntity = genome.ToEntity();

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