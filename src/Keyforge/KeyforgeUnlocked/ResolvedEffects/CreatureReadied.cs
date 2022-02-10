using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
    public sealed class CreatureReadied : ResolvedEffectWithIdentifiable<CreatureReadied>
    {
        public CreatureReadied(IIdentifiable creature) : base(creature)
        {
        }

        public override string ToString()
        {
            return $"{Id.Name} ready";
        }
    }
}