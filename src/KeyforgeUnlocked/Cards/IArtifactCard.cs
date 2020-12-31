using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
  public interface IArtifactCard : ICard
  {
    Trait[] CardTraits { get; }
    Callback CardActionAbility { get; }
  }
}