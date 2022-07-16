using System.Collections.Generic;

namespace RollTheDiceGmtk2022.Game
{


    public class Card
    {
        public Card(CardDefinition definition)
        {
            //todo: create card from definition
        }

        public int Hp { get; set; }
        public List<CardSlot> Slots { get; set; } = new List<CardSlot>();
    }

    public class CardSlot
    {
        public DiceMatchRule Rule { get; set; }
        public CardSlotEffect Effect { get; set; }
    }


}
