using Microsoft.VisualStudio.TestTools.UnitTesting;
using RollTheDiceGmtk2022.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RollTheDiceGmtk2022.Test
{
    [TestClass]
    public class GameStateTests
    {
        [TestClass]
        public class AdvanceGameState
        {
            [TestMethod]
            public void AdvancesUntilEndOfGame()
            {
                var placedCards = new Dictionary<int, Card?>();
                for (var i = 0; i < 3; i++)
                {
                    var attack = new CardSlotEffect();
                    attack.Type = CardSlotEffectType.Attack;
                    attack.Amount = 10;
                    attack.Name = "Backstab";

                    var rouge = new CardDefinition();
                    rouge.Name = "Rogue";
                    rouge.Hp = 25;
                    rouge.Slots.Add(new SlotDefinition { Name = "Backstab", Effect = attack });
                    var card = new Card(rouge, i * 50);
                    card.Slots[0].Rule = DiceMatchRule.Even;
                    placedCards.Add(i, card);

                    //PlacedCards.Add(i, null); //todo
                }
                placedCards.Add(3, null);
                placedCards.Add(4, null);

                var gs = new GameState();
                gs.DiceOracle = new List<DiceMatchRule> { DiceMatchRule.Even, DiceMatchRule.Odd, DiceMatchRule.Even, DiceMatchRule.Odd, DiceMatchRule.Even };
                gs.PlayerHand = new CardHand(placedCards);

                var effect = new CardSlotEffect();
                effect.Type = CardSlotEffectType.Attack;
                effect.Amount = 10;
                effect.Name = "Backstab";

                var boss = new CardDefinition();
                boss.Name = "Enemy Rogue";
                boss.Hp = 250;
                boss.Slots.Add(new SlotDefinition { Name = "Backstab", Effect = effect });
                gs.EnemyCard = new Card(boss, 999);
                gs.EnemyCard.Slots[0].Rule = DiceMatchRule.Odd;

                while (!gs.IsGameEnded)
                    gs.AdvanceGameStateOneTick();

            }
        }
    }
}
