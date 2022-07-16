using System.Collections.Generic;

namespace RollTheDiceGmtk2022.Game
{
    public static class DiceMatchRuleReader{

        public static IReadOnlyCollection<DiceMatchRule> GetMatchingRules(int number)
        {
            //todo:
            List<DiceMatchRule> rules = new List<DiceMatchRule>();
            if (number % 2 == 0)
                rules.Add(DiceMatchRule.Even);
            return rules;
        }

    }


}
