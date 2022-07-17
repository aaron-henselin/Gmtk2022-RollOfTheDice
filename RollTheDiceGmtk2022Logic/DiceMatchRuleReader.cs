using System.Collections.Generic;

namespace RollTheDiceGmtk2022.Game
{
    public static class DiceMatchRuleReader{

        public static IReadOnlyCollection<DiceMatchRule> GetMatchingRules(int roll)
        {
            var matchingRules = new List<DiceMatchRule>();
            
            if (DiceMatchRuleArbiter.SatisfiesRule(DiceMatchRule.Even, roll))
                matchingRules.Add(DiceMatchRule.Even);
            if (DiceMatchRuleArbiter.SatisfiesRule(DiceMatchRule.Odd, roll))
                matchingRules.Add(DiceMatchRule.Odd);
            if (DiceMatchRuleArbiter.SatisfiesRule(DiceMatchRule.High, roll))
                matchingRules.Add(DiceMatchRule.High);
            if (DiceMatchRuleArbiter.SatisfiesRule(DiceMatchRule.Low, roll))
                matchingRules.Add(DiceMatchRule.Low);
            if (DiceMatchRuleArbiter.SatisfiesRule(DiceMatchRule.Six, roll))
                matchingRules.Add(DiceMatchRule.Six);

            return matchingRules;
        }

    }


}
