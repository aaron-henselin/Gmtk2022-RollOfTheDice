using System.Collections.Generic;

namespace RollTheDiceGmtk2022.Game
{
    public class CardHand
    {
        public CardHand()
        {

        }

        public CardHand(Dictionary<int,Card> cardsByPosition)
        {
            foreach (var kvp in cardsByPosition)
            {
                Cards.Add(kvp.Key, kvp.Value == null ? null : new Card(kvp.Value));
            }
            for (int i = 0; i < 6; i++)
            {
                if (!cardsByPosition.ContainsKey(i))
                    cardsByPosition.Add(i, null);
            }
        }

        public Dictionary<int, Card> Cards { get; set; } = new Dictionary<int, Card>();


    }


}
