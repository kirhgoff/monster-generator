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
