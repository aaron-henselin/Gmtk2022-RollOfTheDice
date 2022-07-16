using Microsoft.VisualStudio.TestTools.UnitTesting;
using RollTheDiceGmtk2022.Game;

namespace RollTheDiceGmtk2022.Test
{
    [TestClass]
    public class DiceMatchRuleArbiterTests
    {
        [TestClass]
        public class SatisfiesRule
        {
            [TestMethod]
            public void IdentifiesHighRolls()
            {
                foreach (var roll in new[] { 4, 5, 6 })
                    Assert.IsTrue(DiceMatchRuleArbiter.SatisfiesRule(DiceMatchRule.High, roll));

                foreach (var roll in new[] { 1, 2, 3 })
                    Assert.IsFalse(DiceMatchRuleArbiter.SatisfiesRule(DiceMatchRule.High, roll));
            }

            [TestMethod]
            public void IdentifiesEvens()
            {
                foreach (var roll in new[] { 2, 4, 6 })
                    Assert.IsTrue(DiceMatchRuleArbiter.SatisfiesRule(DiceMatchRule.Even, roll));

                foreach (var roll in new[] { 1, 3, 5 })
                    Assert.IsFalse(DiceMatchRuleArbiter.SatisfiesRule(DiceMatchRule.Even, roll));
            }

            [TestMethod]
            public void IdentifiesOdds()
            {
                foreach (var roll in new[] { 2, 4, 6 })
                    Assert.IsFalse(DiceMatchRuleArbiter.SatisfiesRule(DiceMatchRule.Odd, roll));

                foreach (var roll in new[] { 1, 3, 5 })
                    Assert.IsTrue(DiceMatchRuleArbiter.SatisfiesRule(DiceMatchRule.Odd, roll));
            }
        }


    }
}