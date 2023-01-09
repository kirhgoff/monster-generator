public class TreeNodePrinter
{
    Dictionary<Symbol, string> embodiment = new Dictionary<Symbol, string>();

    public TreeNodePrinter(string rules)
    {
        ParseRules(rules);
    }

    public void ParseRules(string input)
    {
        RulesParser parser = new RulesParser();

        List<string> lines = input
            .Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
            .Select(s => s.Trim())
            .Where(s => s != "")
            .ToList();

        foreach (string line in lines)
        {
            string[] parts = line.Split(new string[] { "=>" }, StringSplitOptions.None);

            Symbol key = parser.ParseSymbols(parts[0].Trim())[0];

            embodiment.Add(key, parts[1].Trim());
        }
    }
    public void Print(TreeNode<Symbol> root)
    {
        Print(root, 0);
    }

    private void Print(TreeNode<Symbol> root, int shift)
    {
        for (int i = 0; i < shift; i++)
        {
            Console.Write(" ");
        }
        Console.WriteLine(root.data.ToString());

        foreach (TreeNode<Symbol> child in root.children)
        {
            Print(child, shift + 1);
        }
    }
}
