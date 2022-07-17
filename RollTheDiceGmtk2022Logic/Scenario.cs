using RollTheDiceGmtk2022.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollTheDiceGmtk2022Logic
{
    public class Scenario
    {
        public string Name { get; set; }
        public List<DiceMatchRule> Oracle { get; set; }
        public List<DiceMatchRule> DiceMatchRulePool { get; set; }
        public List<CardDefinition> AllowedCardDefinitions { get; set; } = new List<CardDefinition>();
        public Card EnemyCard { get; set; }
    }

    public class ScenarioRegistrar
    {
        public Dictionary<int, Scenario> Scenarios = new Dictionary<int, Scenario>();

        //ideas:
        // (0) a scenario with a paladin, rogue combo to double-activate the stab
        // (1) a scenario with just rogues, and the best option is to activate evade more than once per turn.
        // (2) a scenario with a cleric and an archer, where the cleric needs to use a small heal on themselves along with a buff whenever possible.
        // (3) a scenario with a cleric, where the cleric needs to use a "big heal" on the other character often, and a "small heal" on themselves occasionally.
        // (4) a scenario with a paladin, where "command" needs to be chained all the way to the back row to "triple-activate"
        // (5) a scenario with a blind oracle.
        // (6) Hydra -- heals, so dps matters.
        // (7) a scenario that uses the idea that 4,5,6 has two evens but 1,2,3 has 1
        public ScenarioRegistrar()
        {
            Scenarios.Add(0, new Scenario
            {
                AllowedCardDefinitions = new List<CardDefinition> {
                    KnownCardDefinitions.Paladin,
                    KnownCardDefinitions.Rogue,
                },
                DiceMatchRulePool = new List<DiceMatchRule> { DiceMatchRule.Even, DiceMatchRule.Odd, DiceMatchRule.Six },
                Oracle = new List<DiceMatchRule> { DiceMatchRule.Even, DiceMatchRule.Even, DiceMatchRule.Odd },
                EnemyCard = EnemyCardFactory.BuildPlagueRats()
            });

            Scenarios.Add(1, new Scenario
            {
                AllowedCardDefinitions = new List<CardDefinition> {
                    KnownCardDefinitions.Paladin,
                    KnownCardDefinitions.Rogue,
                },
                DiceMatchRulePool = new List<DiceMatchRule> { DiceMatchRule.Even, DiceMatchRule.Even, DiceMatchRule.Even, DiceMatchRule.Odd },
                Oracle = new List<DiceMatchRule> { DiceMatchRule.Even, DiceMatchRule.Even, DiceMatchRule.Odd },
                EnemyCard = EnemyCardFactory.BuildHydra()
            });
        }
    }

}

