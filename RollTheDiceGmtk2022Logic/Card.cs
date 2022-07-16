using System.Collections.Generic;
using System.Linq;

namespace RollTheDiceGmtk2022.Game
{


    public class Card
    {
        public Card(CardDefinition definition, int id)
        {
            Name = definition.Name;
            Hp = definition.Hp;
            Id = id;
            Slots = definition.Slots.Select(x => new CardSlot(x)).ToList();
        }

        public Card(Card otherCard)
        {
            this.Name = otherCard.Name;
            this.Hp = otherCard.Hp;
            this.Id = otherCard.Id;
            this.Slots = otherCard.Slots;


        }

        public Card()
        {

        }

        public string Name { get; set; }
        public int Hp { get; set; }
        public int Id { get; private set; }
        public List<CardSlot> Slots { get; set; } = new List<CardSlot>();

        public List<decimal> DamageBuffs = new List<decimal>();

        public int ShieldWallCount { get; set; } = 0;

        public bool IsDead => Hp <= 0;
    }

    public class CardSlot
    {
        public CardSlot(SlotDefinition definition)
        {
            Effect = definition.Effect;
        }

        public CardSlot(CardSlot slot)
        {
            this.Rule = slot.Rule;
            this.Effect = new CardSlotEffect(slot.Effect);
        }

        public CardSlot()
        {

        }

        public DiceMatchRule? Rule { get; set; }
        public CardSlotEffect Effect { get; set; }
    }


}
