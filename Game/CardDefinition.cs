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
        public string Name { get; set; }
        public CardSlotEffect Effect { get; set; }
    }

}
