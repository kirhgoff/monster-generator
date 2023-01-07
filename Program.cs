using System;

//--------------------------------

public class Symbol 
{
	string face;

	public Symbol(string face) {
		this.face = face;
	}

	public override string ToString()
	{
		return "[" + face + "]";
	}
}

//--------------------------------
public enum RuleType {
    Expanding,
    Continuing
}


public static class RuleTypeExtensions
{
    public static string Character(this RuleType ruleType) 
    {
        if (ruleType == RuleType.Expanding) {
            return "*";
        } else if (ruleType == RuleType.Continuing) {
            return "-";
        } else {
            throw new Exception("Invalid rule type");
        }
    }

    public static bool Matches(this RuleType ruleType, string ruleString) 
    {
        return ruleString.Contains(ruleType.ToString());
    }

    public static RuleType GetRuleType(this string ruleString) 
    {
        if (ruleString.Contains(RuleType.Expanding.Character())) {
            return RuleType.Expanding;
        } else if (ruleString.Contains(RuleType.Continuing.Character())) {
            return RuleType.Continuing;
        } else {
            throw new Exception("Invalid rule type");
        }
    }
}

//--------------------------------
// TODO: make struct
public class Production 
{
    public RuleType ruleType;
	public Symbol input;
	public List<Symbol> output;

	public Production(RuleType ruleType, Symbol input, List<Symbol> output) 
	{
        this.ruleType = ruleType;
		this.input = input;
		this.output = output;
	}

	public override string ToString()
	{
		string inputStr = input.ToString();
        
        List<String> outputStrList = output.Select(symbol => symbol.ToString()).ToList();
		string outputStr = string.Join("", outputStrList);

		return inputStr + " " + ruleType.Character() + " " + outputStr;
	}
}

//--------------------------------
public class RulesParser 
{
    public List<Production> Parse(string input)
    {
        List<Production> rules = new List<Production>();

        List<string> lines = input
            .Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
            .Select(s => s.Trim())
            .Where(s => s != "")
            .ToList();

        foreach (string line in lines)
        {
            RuleType ruleType = line.GetRuleType();
            string[] parts = line.Split(new string[] { ruleType.Character() }, StringSplitOptions.None);

            Symbol inputSymbols = ParseSymbols(parts[0].Trim())[0];
            List<Symbol> outputSymbols = ParseSymbols(parts[1].Trim());

            rules.Add(new Production(ruleType, inputSymbols, outputSymbols));
        }   

        return rules;     
    }

    List<Symbol> ParseSymbols(string input)
    {
        List<Symbol> symbols = new List<Symbol>();

        List<String> pieces = input.Split(
            new string[] { "[", "]"},
            StringSplitOptions.None
        ).ToList();

        List<string> nonEmptyStrings = pieces.Where(s => s != "").ToList();

        foreach (string face in nonEmptyStrings)
        {
            symbols.Add(new Symbol(face));
        }

        return symbols;
    }
}

public class TreeNode<T> 
{
    public T data { get; set; }
    public List<TreeNode<T>> children { get; set; }

    public TreeNode(T data)
    {
        this.data = data;
        this.children = new List<TreeNode<T>>();
    }

    public void AddChild(TreeNode<T> child)
    {
        children.Add(child);
    }

    public override string ToString()
    {
        return data?.ToString() ?? "null";
    }
}


class Grammar 
{
    List<Production> rules;

    public Grammar(List<Production> rules)
    {
        this.rules = rules;
    }

    public TreeNode<Symbol> Expand(TreeNode<Symbol> node)
    {
        List<Production> applicableRules = rules
            .Where(rule => rule.input == node.data)
            .ToList();

        foreach (Production rule in applicableRules)
        {
            List<TreeNode<Symbol>> newNodes = rule.output
                .Select(symbol => new TreeNode<Symbol>(symbol))
                .ToList();

            switch (rule.ruleType) 
            {
                case RuleType.Expanding:
                    node.children.AddRange(newNodes);
                    break;
                case RuleType.Continuing:
                    node.children = newNodes;
                    break;
            }
            
            foreach (TreeNode<Symbol> child in node.children)
            {
                Expand(child);
            }
        }

        return node;
    }
 
}

public class Program
{
	public static void Main()
	{
        Test1();
        Test2();
        Test3();
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
        ";

        RulesParser parser = new RulesParser();
        List<Production> rules = parser.Parse(input);

        Grammar grammar = new Grammar(rules);

        TreeNode<Symbol> seed = new TreeNode<Symbol>(new Symbol("seed"));
        TreeNode<Symbol> tree = grammar.Expand(seed);

        Console.WriteLine(tree.ToString());}
}