using System;

public class Program
{
	public static void Main()
	{
        // Test1();
        // Test2();
        // Test3();
        Test4();
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

    static void Test4() 
    {
        SymbolMapper mapper = new SymbolMapper(new Dictionary<string, char> {
            { "head", '.' },
            { "body", 'o' }
        });
        Console.WriteLine("--------- Test4 ---------");
        List<Organella> organellas = new List<Organella>() {
            new Organella(new Symbol("head"), new Shape(0.0, 0.0, 10)),
            new Organella(new Symbol("body"), new Shape(10.0, 10.0, 5))
        };

        string picture = AsciiRenderer.Render(organellas, mapper);
        Console.WriteLine(picture);
    }
}