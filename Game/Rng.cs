using System;

namespace RollTheDiceGmtk2022.Game
{
    public static class Rng
    {
        private static readonly Random _random = new Random(42);

        public static int Roll(DiceMatchRule rule)
        {
            var roll = NextRoll();

            while (!DiceMatchRuleArbiter.SatisfiesRule(rule, roll))
                roll = NextRoll();

            return roll;
        }

        private static int NextRoll(int max = 6)
        {
            if (max % 2 == 1)
                throw new Exception("Can only roll even max numbers");

            return _random.Next(1, max + 1);
        }

        
    }


}
