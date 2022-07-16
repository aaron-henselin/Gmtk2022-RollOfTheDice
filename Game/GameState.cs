using System;
using System.Collections.Generic;
using System.Linq;

namespace RollTheDiceGmtk2022.Game
{
    public class GameState
    {
        public List<DiceOracle> DiceOracles { get; set; }

        
        public int TurnNumber { get; set; } = 0;

        public CardHand PlayerHand { get; set; } = new CardHand();
        public Card EnemyCard { get; set; }

        public void AdvanceGameState()
        {
            var roll = Rng.Roll(DiceOracle.Even);
            var matchingRules = DiceMatchRuleReader.GetMatchingRules(roll);
            var activeCard = PlayerHand.ActiveCard;

            var matchingRulesSet = new HashSet<DiceMatchRule>(matchingRules);
            var playerCardSlotsToActivate = activeCard.Slots.Where(x => matchingRulesSet.Contains(x.Rule));
            foreach (var slotToActivate in playerCardSlotsToActivate)
                RunEffect(activeCard, slotToActivate.Effect);

            //todo: check for game-end states

            var enemyCardSlotsToActivate = EnemyCard.Slots.Where(x => matchingRulesSet.Contains(x.Rule));
            foreach (var slotToActivate in playerCardSlotsToActivate)
                RunEffect(activeCard, slotToActivate.Effect);

        }

        public void RunEffect(Card card, CardSlotEffect effect)
        {
            //todo: actually do the effect
            Console.WriteLine("run effect = "+ effect);
        }

        

    }


}
