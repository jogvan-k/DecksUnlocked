using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public class CreatureHealed : ResolvedEffectWithIdentifiableAndInt<CreatureHealed>
  {
    public CreatureHealed(IIdentifiable creature, int @int) : base(creature, @int)
    {
    }
    
    public override string ToString()
    {
      return $"{Int} damage healed from {Id.Name}";
    }
  }
}