using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ResolvedEffects
{
    public class CreatureExhausted : ResolvedEffectWithIdentifiable<CreatureExhausted>
    {
        public CreatureExhausted(IIdentifiable id) : base(id)
        {
        }

        public override string ToString()
        {
            return $"{Id.Name} exhausted";
        }
    }
}