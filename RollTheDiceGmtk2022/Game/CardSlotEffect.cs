namespace RollTheDiceGmtk2022.Game
{
    public class CardSlotEffect
    {
        public CardSlotEffect()
        {

        }

        public CardSlotEffect(CardSlotEffect otherEffect)
        {
            this.Name = otherEffect.Name;
            this.Type = otherEffect.Type;
            this.Amount = otherEffect.Amount;
        }

        public string Name { get; set; }

        public CardSlotEffectType Type { get; set; }

        public decimal Amount { get; set; }
    }

    public enum CardSlotEffectType
    {
        Attack, Heal
    }
}
