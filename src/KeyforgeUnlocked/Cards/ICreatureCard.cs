using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public interface ICreatureCard : ICard
  {
    int CardPower { get; }
    int CardArmor { get; }
    Keyword[] CardKeywords { get; }
    Trait[] CardTraits { get; }
    Callback CardFightAbility { get; }
    Callback CardBeforeFightAbility { get; }
    Callback CardAfterKillAbility { get; }
    Callback CardCreatureAbility { get; }
    Callback CardReapAbility { get; }
    Callback CardDestroyedAbility { get; }
    ActionPredicate CardUseActionAllowed { get; }
    
    Creature InsantiateCreature();
  }
}