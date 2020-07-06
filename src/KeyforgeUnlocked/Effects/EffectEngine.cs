using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public class EffectEngine
  {
    State _state;

    public EffectEngine(State state)
    {
      _state = state;
    }

    public State Draw(Player player, int cards = 1)
    {
      if (cards <= 0)
      {
        return _state;
      }

      _state.Decks.TryReduce(player, cards, out var newDecks, out var extraCards);
      var newHands = new Dictionary<Player, Card[]>
        {{player, _state.Hands[player].Concat(extraCards)}, {player.Other(), _state.Hands[player.Other()]}};

      return _state.New(decks: newDecks, hands: newHands);
    }
  }
}