using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
    public interface IEffect
    {
        public void Resolve(IMutableState state);
    }
}