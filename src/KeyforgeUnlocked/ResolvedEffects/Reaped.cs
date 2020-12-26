using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class Reaped : ResolvedEffectWithIdentifiable<Reaped>
  {
    public Reaped(IIdentifiable creature) : base(creature)
    {
    }

    public override string ToString()
    {
      return $"{Id.Name} reaped";
    }
  }
}