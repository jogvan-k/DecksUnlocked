using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class PlayCreature : Effect
  {
    readonly CreatureCard _card;
    readonly int _position;

    public PlayCreature(Player player,
      CreatureCard card,
      int position) : base(player)
    {
      _card = card;
      _position = position;
    }

    public override void Resolve(MutableState state)
    {
      ValidatePosition(state);
      state.Fields[Player].Insert(_position, _card.InsantiateCreature());

      if(!state.Hands[Player].Remove(_card))
        throw new CardNotPresentException(state);
    }

    void ValidatePosition(IState state)
    {
      var creaturesOnField = state.Fields[Player].Count;
      if (!(0 <= _position && _position <= creaturesOnField))
        throw new InvalidBoardPositionException(state, _position);
    }
  }
}