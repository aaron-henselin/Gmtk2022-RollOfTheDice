using System;

namespace RollTheDiceGmtk2022.Game
{
    public enum DiceMatchRule
    {
        Even, Odd, High, Low
    }

    public static class DiceMatchRuleArbiter
    {
        public static bool SatisfiesRule(DiceMatchRule rule, int roll, int max = 6)
        {
            if (max % 2 == 1)
                throw new Exception("Can only roll even max numbers");

            switch (rule)
            {
                case DiceMatchRule.Even:
                    return roll % 2 == 0;
                case DiceMatchRule.Odd:
                    return roll % 1 == 1;
                case DiceMatchRule.High:
                    return roll > (max / 2);
                case DiceMatchRule.Low:
                    return roll <= (max / 2);
                default:
                    throw new ArgumentOutOfRangeException("Unknown rule");
            }
        }
    }
}
