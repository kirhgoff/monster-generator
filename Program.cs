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
		return face;
	}
}

//--------------------------------

class Production 
{
	List<Symbol> input;
	List<Symbol> output;

	public Production(List<Symbol> input, List<Symbol> output) 
	{
		this.input = input;
		this.output = output;
	}

	public override string ToString()
	{
        List<String> inputStrList = input.Select(symbol => symbol.ToString()).ToList();
		string inputStr = string.Join("", inputStrList);
        
        List<String> outputStrList = output.Select(symbol => symbol.ToString()).ToList();
		string outputStr = string.Join("", outputStrList);

		return inputStr + " => " + outputStr;
	}
}

public class Program
{
	public static void Main()
	{
		List<Symbol> input = new List<Symbol>{ new Symbol("seed") };
		List<Symbol> output = new List<Symbol>{ new Symbol("head"), new Symbol("body") };
		Production prod1 = new Production(input, output);

		Production[] rules = 
		{
			prod1
		};

		Console.WriteLine(prod1.ToString());
	}
}