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

        public GameStateTimer Timer => timer;

        public List<string> Log { get; set; } = new List<string>();

        public List<DiceMatchRule> DiceOracle { get; set; } = new List<DiceMatchRule>();

        public CardHand PlayerHand { get; set; } = new CardHand();
        public Card EnemyCard { get; set; }

        public bool IsEnemyCardDefeated => this.EnemyCard.IsDead;
        public bool IsPlayerHandDefeated => this.PlayerHand.Cards.Values.All(x => x == null || x.IsDead);

        public bool IsGameEnded => IsEnemyCardDefeated || IsPlayerHandDefeated;

        public GameOutcome AdvanceGameStateUntilCompletion(int timeout)
        {
            while (!IsGameEnded && timeout > 0)
            {
                AdvanceGameStateOneTick();
                timeout--;
            }

            return new GameOutcome {
                IsDraw = !IsGameEnded,
                Won = IsEnemyCardDefeated,
                EndingTurn = timer.TurnNumber,
                AllPartyMembersSurvived = !PlayerHand.Cards.Any(x => x.Value != null && x.Value.IsDead)
            };

        }

        public bool AdvanceGameStateOneTick()
        {
            var thisOracle = DiceOracle[timer.DiceIndex];
           
            AdvanceGameStateByPhase(thisOracle);


            //todo: check for game-end states and break;
            if (IsGameEnded)
                return true;

            timer.DiceIndex++;
            if (timer.DiceIndex == 5)
            {
                timer.DiceIndex = 0;
                timer.TurnNumber++;
            }

            Log.Add($"Turn={timer.TurnNumber},DiceIndex={timer.DiceIndex}");

            return false;
        }


        public void AdvanceGameStateByPhase(DiceMatchRule rule)
        {

            var roll = Rng.Roll(rule);
            Log.Add("Roll: " + roll);
            
            var matchingRules = DiceMatchRuleReader.GetMatchingRules(roll);
            var matchingRulesSet = new HashSet<DiceMatchRule>(matchingRules);

            Log.Add("Rules: " + string.Join(",",matchingRules));

            var activePlayerCard = PlayerHand.Cards[timer.DiceIndex];
            if (activePlayerCard != null && !activePlayerCard.IsDead)
            {
                Log.Add("Player card activating:" + activePlayerCard.Id);
                var playerCardSlotsToActivate = activePlayerCard.Slots.Where(x => x.Rule != null && matchingRulesSet.Contains(x.Rule.Value)).ToList();
                Log.Add("Slot count activating:" + playerCardSlotsToActivate.Count);
                foreach (var slotToActivate in playerCardSlotsToActivate)
                    RunEffect(activePlayerCard, slotToActivate.Effect, EnemyCard);
            }
            else
            {
                Log.Add("Player Card is dead, or there is no card at " + timer.DiceIndex);
            }

            if (!EnemyCard.IsDead)
            {
                Log.Add("EnemyCard activating:" + EnemyCard.Id);
                var enemyCardSlotsToActivate = EnemyCard.Slots.Where(x => x.Rule != null && matchingRulesSet.Contains(x.Rule.Value)).ToList();
                Log.Add("Slot count activating:" + enemyCardSlotsToActivate.Count);
                foreach (var slotToActivate in enemyCardSlotsToActivate)
                    RunEffect(EnemyCard, slotToActivate.Effect, activePlayerCard);
            }

        }

        public void RunEffect(Card source, CardSlotEffect effect, Card target)
        {
            if (target == null)
                return;

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

    public class GameOutcomeProbability
    {
        public GameOutcomeProbability(List<GameOutcome> outcomes)
        {
            var totalOutcomeCount = outcomes.Count;
            IsDraw = outcomes.Count(x => x.IsDraw) / (decimal)totalOutcomeCount;
            Won = outcomes.Count(x => x.Won) / (decimal)totalOutcomeCount;
            AllPartyMembersSurvived = outcomes.Count(x => x.AllPartyMembersSurvived) / (decimal)totalOutcomeCount;
        }

        public decimal IsDraw { get; set; }
        public decimal Won { get; set; }
        public decimal AllPartyMembersSurvived { get; set; }
    }

    public class GameOutcome
    {
        public bool IsDraw { get; set; }
        public int EndingTurn { get; set; }
        public bool Won { get; set; }
        public bool AllPartyMembersSurvived { get; internal set; }
    }



}
