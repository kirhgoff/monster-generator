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
            .Where(rule => {
                // Console.WriteLine("Checking input: " + rule.input.ToString() + " against node: " + node.ToString());
                return rule.input.Equals(node.data);
            })
            .ToList();

        // Console.WriteLine("Applicable rules: " + applicableRules.Count);

        foreach (Production rule in applicableRules)
        {
            // Console.WriteLine("Passing rule: " + rule.ToString());

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
                // Console.WriteLine("Passing child: " + child.ToString());
                Expand(child);
            }
        }

        return node;
    }
}
