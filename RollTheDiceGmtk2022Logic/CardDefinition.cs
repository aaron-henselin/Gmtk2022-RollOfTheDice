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
        public static CardSlotEffect TollOfTheDead = new CardSlotEffect { Amount = 2m, Type = CardSlotEffectType.DamageBuff, Name = "Toll Of the Dead", Description = "Permanent DMG * 2 for the character in the next position." };

        public static CardSlotEffect Spear = new CardSlotEffect { Amount = 10, Type = CardSlotEffectType.Attack, Name = "Spear", Description = "Spear" };


        public static CardSlotEffect HydraAttack = new CardSlotEffect { Amount = 50, Type = CardSlotEffectType.Attack, Name = "Chomp", Description = "You dodged 8 heads! Ouch." };
        public static CardSlotEffect HydraHeal = new CardSlotEffect { Amount = 15, Type = CardSlotEffectType.Heal, Name = "Regenerate", Description = "The Hydra's legendary healing is largely embellished" };

        public static CardSlotEffect CyclopsAttack = new CardSlotEffect { Amount = 5, Type = CardSlotEffectType.Attack, Name = "Smack", Description = "Playful, but still punishing!" };
        public static CardSlotEffect CyclopsDeath = new CardSlotEffect { Amount = 777, Type = CardSlotEffectType.Attack, Name = "Overhead Smash", Description = "Turns you into 'No Man' very quickly." };
    }

    public class EnemyCardFactory
    {
        public static Card BuildPlagueRats()
        {
            var rats = new Card(KnownCardDefinitions.PlagueRats, -1);
            rats.Slots[0].Rule = DiceMatchRule.Six;
            rats.Slots[1].Rule = DiceMatchRule.Low;
            return rats;
        }
        public static Card BuildHydra()
        {
            var rats = new Card(KnownCardDefinitions.Hydra, -1);
            rats.Slots[0].Rule = DiceMatchRule.Even;
            rats.Slots[1].Rule = DiceMatchRule.Odd;
            return rats;
        }

        public static Card BuildHydraPlusPlus()
        {
            var rats = new Card(KnownCardDefinitions.HydraPlusPlus, -1);
            rats.Slots[0].Rule = DiceMatchRule.Even;
            rats.Slots[1].Rule = DiceMatchRule.Odd;
            return rats;
        }

        public static Card BuildCyclops()
        {
            var cyclops = new Card(KnownCardDefinitions.Cyclops, -1);
            cyclops.Slots[0].Rule = DiceMatchRule.Odd;
            cyclops.Slots[1].Rule = DiceMatchRule.Six;
            return cyclops;
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
                }
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
                    Effect = KnownCardSlotEffects.Spear
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.ShieldWall
                },
            }
        };
        public static CardDefinition Cleric = new CardDefinition
        {
            Name = "Cleric",
            Hp = 75,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.LayOnHands
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.TollOfTheDead
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.Spear
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
                }
            }
        };

        public static CardDefinition Hydra = new CardDefinition
        {
            Name = "Hydra",
            Hp = 100,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.HydraAttack
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.HydraHeal
                }
            }
        };

        public static CardDefinition HydraPlusPlus = new CardDefinition
        {
            Name = "Hydra++",
            Hp = 400,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.HydraAttack
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.HydraHeal
                }
            }
        };

        public static CardDefinition Cyclops = new CardDefinition
        {
            Name = "Cyclops",
            Hp = 100,
            Slots = new List<SlotDefinition> {
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.CyclopsAttack
                },
                new SlotDefinition {
                    Effect = KnownCardSlotEffects.CyclopsDeath
                }
            }
        };

    }

}
