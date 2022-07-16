using System.Collections.Generic;

namespace RollTheDiceGmtk2022.Game
{


    public class Card
    {
        public Card(CardDefinition definition)
        {
            Hp = definition.Hp;
        }

        public int Hp { get; set; }
        public List<CardSlot> Slots { get; set; } = new List<CardSlot>();

        public bool IsDead => Hp <= 0;
    }

    public class CardSlot
    {
        public CardSlot(SlotDefinition definition)
        {
            Effect = definition.Effect;
        }

        public DiceMatchRule? Rule { get; set; }
        public CardSlotEffect Effect { get; set; }
    }


}
