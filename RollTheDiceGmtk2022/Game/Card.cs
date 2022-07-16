using System.Collections.Generic;
using System.Linq;

namespace RollTheDiceGmtk2022.Game
{


    public class Card
    {
        public Card(CardDefinition definition, int id)
        {
            Hp = definition.Hp;
            Id = id;
            Slots = definition.Slots.Select(x => new CardSlot(x)).ToList();
        }

        public Card(Card otherCard)
        {
            this.Hp = otherCard.Hp;
            this.Id = otherCard.Id;
            this.Slots = otherCard.Slots;


        }

        public int Hp { get; set; }
        public int Id { get; private set; }
        public List<CardSlot> Slots { get; set; } = new List<CardSlot>();

        public List<decimal> DamageBuffs = new List<decimal>();

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

        public DiceMatchRule? Rule { get; set; }
        public CardSlotEffect Effect { get; set; }
    }


}
