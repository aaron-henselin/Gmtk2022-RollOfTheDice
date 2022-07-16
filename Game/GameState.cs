using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RollTheDiceGmtk2022.Game
{

    public class GameStateTimer
    {
        public int TurnNumber { get; set; } = 0;
        public int DiceIndex { get; set; } = 0;
    }
    

    public class GameState
    {
        GameStateTimer timer = new GameStateTimer();

        public List<DiceMatchRule> DiceOracle { get; set; }
        
        public CardHand PlayerHand { get; set; } = new CardHand();
        public Card EnemyCard { get; set; }

        public bool IsEnemyCardDefeated => this.EnemyCard.IsDead;
        public bool IsPlayerHandDefeated => this.PlayerHand.Cards.All(x => x.IsDead);

        public bool IsGameEnded => IsEnemyCardDefeated || IsPlayerHandDefeated;

        public bool AdvanceGameState()
        {
            var thisOracle = DiceOracle[timer.DiceIndex];
            AdvanceGameStateByPhase(thisOracle);

            //todo: check for game-end states and break;
            if (IsGameEnded)
                return true;

            timer.DiceIndex++;
            if (timer.DiceIndex == 6)
            {
                timer.DiceIndex = 0;
                timer.TurnNumber++;
            }

            return false;
        }


        public void AdvanceGameStateByPhase(DiceMatchRule rule)
        {

            var roll = 0;// Rng.Roll(rule);
            var roll = Rng.Roll(DiceMatchRule.Even);
            var matchingRules = DiceMatchRuleReader.GetMatchingRules(roll);
            var activeCard = PlayerHand.ActiveCard;

            var matchingRulesSet = new HashSet<DiceMatchRule>(matchingRules);
            var playerCardSlotsToActivate = activeCard.Slots.Where(x => matchingRulesSet.Contains(x.Rule));
            foreach (var slotToActivate in playerCardSlotsToActivate)
                RunEffect(activeCard, slotToActivate.Effect, EnemyCard);

            var enemyCardSlotsToActivate = EnemyCard.Slots.Where(x => matchingRulesSet.Contains(x.Rule));
            foreach (var slotToActivate in playerCardSlotsToActivate)
                RunEffect(EnemyCard, slotToActivate.Effect, activeCard);

            PlayerHand.Pop();

        }

        public void RunEffect(Card source, CardSlotEffect effect, Card target)
        {
            switch (effect.Type)
            {
                case CardSlotEffectType.Attack:
                    target.Hp -= (int) effect.Amount;
                    break;
                case CardSlotEffectType.Heal:
                    source.Hp += (int)effect.Amount;
                    break;
            }
            
        }



    }


}
