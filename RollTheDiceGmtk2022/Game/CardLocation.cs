namespace RollTheDiceGmtk2022.Game
{
    public struct CardLocation
    {
        public CardLocation(bool inHand, int collectionIndex = 0)
        {
            InHand = inHand;
            CollectionIndex = collectionIndex;
        }

        public bool InHand { get; set; }

        public int CollectionIndex { get; set; }
    }
}
