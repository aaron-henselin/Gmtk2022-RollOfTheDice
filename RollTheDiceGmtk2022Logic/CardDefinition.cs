using System.Collections.Generic;

namespace RollTheDiceGmtk2022.Game
{
    public class CardDefinition
    {
        public string Name { get; set; }
        public int Hp { get; set; }
        public List<SlotDefinition> Slots { get; set; } = new List<SlotDefinition>();
    }

    public class SlotDefinition
    {
        public string Name => Effect.Name;
        public CardSlotEffect Effect { get; set; }
    }

    public class KnownCardSlotEffects
    {
        public static CardSlotEffect Bite = new CardSlotEffect { Amount = 5, Type = CardSlotEffectType.Attack, Name = "Bite" };
        public static CardSlotEffect Munch = new CardSlotEffect { Amount = 20, Type = CardSlotEffectType.Attack, Name = "Munch" };

        public static CardSlotEffect Backstab = new CardSlotEffect { Amount = 25, Type = CardSlotEffectType.Attack, Name="Backstab" };
        public static CardSlotEffect Evade = new CardSlotEffect { Amount = 25, Type = CardSlotEffectType.Evade, Name = "Evade",Description="Switch positions with the character in the back row, the character in the back row will take the turn instead." };
        public static CardSlotEffect Command = new CardSlotEffect { Amount = 10, Type = CardSlotEffectType.Command, Name="Command",Description = "Execute the action of the character in the back row." };
        public static CardSlotEffect ShieldWall = new CardSlotEffect { Amount = 10, Type = CardSlotEffectType.ShieldWall, Name = "Shield Wall", Description = "Take 0 damage from the first attack this turn." };

        public static CardSlotEffect Longbow = new CardSlotEffect { Amount = 20, Type = CardSlotEffectType.Attack, Name = "Longbow" };
        public static CardSlotEffect LayOnHands = new CardSlotEffect { Amount = 30, Type = CardSlotEffectType.Heal, Name = "Lay On Hands",Description = "Heal the character in the back row." };
        public static CardSlotEffect HealingHerbsAndSpices = new CardSlotEffect { Amount = 10, Type = CardSlotEffectType.Heal, Name = "Healing Herbs And Spices", Description = "Heal the character in the back row." };
        public static CardSlotEffect TollOfTheDead = new CardSlotEffect { Amount = 1.1m, Type = CardSlotEffectType.DamageBuff, Name = "DMG +", Description = "DMG * 1.1 for the character in the back row" };
    }

    public class EnemyCardFactory
    {
        public static Card BuildPlagueRats()
        {
            var rats = new Card(KnownCardDefinitions.PlagueRats, -1);
            rats.Slots[0].Rule = DiceMatchRule.Low;
            rats.Slots[1].Rule = DiceMatchRule.Six;
            return rats;
        }
    }


    public class KnownCardDefinitions
    {
        public static CardDefinition Rogue = new CardDefinition {
            Hp = 25,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Backstab
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Evade
                },

            }
        };

        public static CardDefinition Archer = new CardDefinition
        {
            Hp = 50,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Longbow
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.HealingHerbsAndSpices
                },
            }

        };
        public static CardDefinition Paladin = new CardDefinition
        {
            Hp = 100,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Command
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.ShieldWall
                },
            }
        };
        public static CardDefinition Cleric = new CardDefinition
        {
            Hp = 50,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.LayOnHands
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.TollOfTheDead
                },
            }
        };

        public static CardDefinition PlagueRats = new CardDefinition
        {
            Hp = 50,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Bite
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Munch
                },
            }
        };

    

    }

}
