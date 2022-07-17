﻿using System.Collections.Generic;

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
        public static CardSlotEffect Bite = new CardSlotEffect { Amount = 20, Type = CardSlotEffectType.Attack, Name = "Bite", Description = "A vicious bite." };
        public static CardSlotEffect Munch = new CardSlotEffect { Amount = 5, Type = CardSlotEffectType.Attack, Name = "Nibble", Description = "Honestly, just teeth marks." };

        public static CardSlotEffect Backstab = new CardSlotEffect { Amount = 25, Type = CardSlotEffectType.Attack, Name = "Stab", Description = "Stabbing (the front)" };
        public static CardSlotEffect Evade = new CardSlotEffect { Amount = 25, Type = CardSlotEffectType.Evade, Name = "Evade", Description = "This character moves to the next position (if empty)." };
        public static CardSlotEffect Command = new CardSlotEffect { Amount = 10, Type = CardSlotEffectType.Command, Name = "Command", Description = "Command the character in the next position to act." };
        public static CardSlotEffect ShieldWall = new CardSlotEffect { Amount = 10, Type = CardSlotEffectType.ShieldWall, Name = "Shield Wall", Description = "First attack this turn deals no damage." };

        public static CardSlotEffect Longbow = new CardSlotEffect { Amount = 20, Type = CardSlotEffectType.Attack, Name = "Longbow" };

        public static CardSlotEffect LayOnHands = new CardSlotEffect { Amount = 30, Type = CardSlotEffectType.Heal, Name = "Lay On Hands",Description = "Heal the character in the back row." };
        public static CardSlotEffect HealingWords = new CardSlotEffect { Amount = 5, Type = CardSlotEffectType.Heal, Name = "Healing Aura", Description = "Heals all characters." };
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
            Name = "Rogue",
            Hp = 25,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Backstab
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Evade
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Evade
                },
            }
        };

        public static CardDefinition Archer = new CardDefinition
        {
            Name = "Archer",
            Hp = 50,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Longbow
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.HealingHerbsAndSpices
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.HealingHerbsAndSpices
                },
            }

        };
        public static CardDefinition Paladin = new CardDefinition
        {
            Name = "Paladin",
            Hp = 100,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Command
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Halberd
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.ShieldWall
                },
            }
        };
        public static CardDefinition Cleric = new CardDefinition
        {
            Name = "Cleric",
            Hp = 50,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.LayOnHands
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.TollOfTheDead
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.TollOfTheDead
                },
            }
        };

        public static CardDefinition PlagueRats = new CardDefinition
        {
            Name = "Big Rats",
            Hp = 50,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Bite
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Munch
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Munch
                },
            }
        };

    

    }

}
