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

        private void ActivatePlayerCardSlots(Card activePlayerCard, HashSet<DiceMatchRule> matchingRulesSet)
        {
            var playerCardSlotsToActivate = activePlayerCard.Slots.Where(x => x.Rule != null && matchingRulesSet.Contains(x.Rule.Value)).ToList();
            foreach (var slotToActivate in playerCardSlotsToActivate)
            {
                var nextDiceIndex = timer.DiceIndex + 1;
                if (nextDiceIndex == 5)
                    nextDiceIndex = 0;

                if (slotToActivate.Effect.Type == CardSlotEffectType.Command)
                {
                    var otherCard = PlayerHand.Cards[nextDiceIndex];
                    if (otherCard != null && timer.DiceIndex == nextDiceIndex) //can't activate the same card.
                        ActivatePlayerCardSlots(otherCard, matchingRulesSet);
                }
                else
                {
                    Card targetCard;
                    if (slotToActivate.Effect.Type == CardSlotEffectType.Attack)
                        targetCard = EnemyCard;
                    if (slotToActivate.Effect.Type == CardSlotEffectType.Heal ||
                        slotToActivate.Effect.Type == CardSlotEffectType.DamageBuff ||
                        slotToActivate.Effect.Type == CardSlotEffectType.Evade)
                        targetCard = PlayerHand.Cards[nextDiceIndex];

                    RunEffect(activePlayerCard, slotToActivate.Effect, EnemyCard);
                }

            }
        }

        public void AdvanceGameStateByPhase(DiceMatchRule rule)
        {

            var roll = Rng.Roll(rule);
            Log.Add("Roll: " + roll);
            
            var matchingRules = DiceMatchRuleReader.GetMatchingRules(roll);
            var activationCriteria = new HashSet<DiceMatchRule>(matchingRules);

            Log.Add("Rules: " + string.Join(",",matchingRules));

            var activePlayerCard = PlayerHand.Cards[timer.DiceIndex];
            if (activePlayerCard != null && !activePlayerCard.IsDead)
            {
                Log.Add("Player card activating:" + activePlayerCard.Id);
                ActivatePlayerCardSlots(activePlayerCard,activationCriteria);
            }
            else
            {
                Log.Add("Player Card is dead, or there is no card at " + timer.DiceIndex);
            }

            if (!EnemyCard.IsDead)
            {
                Log.Add("EnemyCard activating:" + EnemyCard.Id);
                var enemyCardSlotsToActivate = EnemyCard.Slots.Where(x => x.Rule != null && activationCriteria.Contains(x.Rule.Value)).ToList();
                Log.Add("Slot count activating:" + enemyCardSlotsToActivate.Count);
                foreach (var slotToActivate in enemyCardSlotsToActivate)
                    RunEffect(EnemyCard, slotToActivate.Effect, activePlayerCard);
            }

            //remove one-time effects.
            if (activePlayerCard != null)
                activePlayerCard.ShieldWallCount = 0;
        }

        public void RunEffect(Card source, CardSlotEffect effect, Card target)
        {
            if (target == null)
                return;

            switch (effect.Type)
            {
                case CardSlotEffectType.Attack:
                    if (target.ShieldWallCount > 0)
                    {
                        target.ShieldWallCount--;
                        return;
                    }
                    var damageWithBuff = effect.Amount;
                    foreach (var buff in source.DamageBuffs)
                        damageWithBuff *= buff;
                    target.Hp -= Convert.ToInt32(damageWithBuff);
                    return;
                    
                case CardSlotEffectType.Heal:
                    source.Hp += Convert.ToInt32(effect.Amount);
                    return;

                case CardSlotEffectType.DamageBuff:
                    source.DamageBuffs.Add(effect.Amount);
                    return;

                case CardSlotEffectType.ShieldWall:
                    source.ShieldWallCount++;
                    return;
            }

            //throw new Exception("Unknown effect " + effect);
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
