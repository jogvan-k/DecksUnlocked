using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
    public class CreatureDamaged : ResolvedEffectWithIdentifiableAndInt<CreatureDamaged>
    {
        public CreatureDamaged(IIdentifiable creature, int @int) : base(creature, @int)
        {
        }

        public override string ToString()
        {
            return $"{Int} damage dealt to {Id.Name}";
        }
    }
}