using System.Collections.Generic;

namespace RollTheDiceGmtk2022.Game
{
    public class CardHand
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public Card ActiveCard => Cards[0];

        public void Pop()
        {
            var previouslyActiveCard = ActiveCard;
            Cards.RemoveAt(0);
            Cards.Add(previouslyActiveCard);
        }
    }


}
