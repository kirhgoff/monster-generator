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
