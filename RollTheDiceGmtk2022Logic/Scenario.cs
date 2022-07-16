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

        public ScenarioRegistrar()
        {
            Scenarios.Add(0, new Scenario
            {
                AllowedCardDefinitions = new List<CardDefinition> {
                    KnownCardDefinitions.Paladin,
                    KnownCardDefinitions.Cleric,
                },
                DiceMatchRulePool = new List<DiceMatchRule> { DiceMatchRule.Even, DiceMatchRule.Odd, DiceMatchRule.Six, DiceMatchRule.High },
                Oracle = new List<DiceMatchRule> { DiceMatchRule.Even, DiceMatchRule.Odd, DiceMatchRule.Even, DiceMatchRule.Odd },
                EnemyCard = EnemyCardFactory.BuildPlagueRats()
            });
        }
    }

}

