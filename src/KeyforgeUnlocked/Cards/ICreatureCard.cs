using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public interface ICreatureCard : ICard
  {
    int CardPower { get; }
    int CardArmor { get; }
    Keyword[] CardKeywords { get; }
    CreatureType[] CardTypes { get; }
    Callback CardFightAbility { get; }
    Callback CardBeforeFightAbility { get; }
    Callback CardAfterKillAbility { get; }
    Callback CardCreatureAbility { get; }
    Callback CardReapAbility { get; }
    Callback CardDestroyedAbility { get; }
    
    Creature InsantiateCreature();
  }
}