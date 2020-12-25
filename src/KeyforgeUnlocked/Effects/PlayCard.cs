using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;

namespace KeyforgeUnlocked.Effects
{
  public abstract class PlayCard<T> : EffectWithCard<T> where T : PlayCard<T>
  {
    protected void ResolvePlayEffects(MutableState state)
    {
      foreach (var pip in Card.Pips)
      {
        state.ResolvePip(pip);
      }

      Card.CardPlayAbility?.Invoke(state, Card.Id);
    }
    protected PlayCard(ICard card) : base(card)
    {
    }
  }
}