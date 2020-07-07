using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public class PlayCreature : Effect
  {
    Player _playingPlayer;
    CreatureCard _card;
    int _position;

    public PlayCreature(Player playingPlayer,
      CreatureCard card,
      int position)
    {
      _playingPlayer = playingPlayer;
      _card = card;
      _position = position;
    }

    public override void Resolve(MutableState state)
    {
      ValidatePosition(state);
      state.Fields[_playingPlayer].Insert(_position, _card.InsantiateCreature());

      if(!state.Hands[_playingPlayer].Remove(_card))
        throw new CardNotPresentException(state);
    }

    void ValidatePosition(State state)
    {
      var creaturesOnField = state.Fields[_playingPlayer].Count;
      if (!(0 <= _position && _position <= creaturesOnField))
        throw new InvalidBoardPositionException(state, _position);
    }
  }
}