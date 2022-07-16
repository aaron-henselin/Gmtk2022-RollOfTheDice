namespace RollTheDiceGmtk2022.Game
{
    public struct DiceMatchRuleLocation
    {
        public DiceMatchRuleLocation(bool inDashboard, int collectionIndex = 0, int slotIndex = 0)
        {
            InDashboard = inDashboard;
            CollectionIndex = collectionIndex;
            SlotIndex = slotIndex;
        }

        public bool InDashboard { get; set; }

        public int CollectionIndex { get; set; }

        public int SlotIndex { get; set; }
    }
}
