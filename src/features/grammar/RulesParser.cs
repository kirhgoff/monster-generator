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

    public List<Symbol> ParseSymbols(string input)
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
