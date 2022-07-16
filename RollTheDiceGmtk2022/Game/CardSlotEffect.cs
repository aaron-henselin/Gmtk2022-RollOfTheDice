namespace RollTheDiceGmtk2022.Game
{
    public class CardSlotEffect
    {
        public string Name { get; set; }

        public CardSlotEffectType Type { get; set; }

        public decimal Amount { get; set; }
    }

    public enum CardSlotEffectType
    {
        Attack, Heal
    }
}
