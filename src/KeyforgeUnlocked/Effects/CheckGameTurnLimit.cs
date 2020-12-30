using System.Linq;
using KeyforgeUnlocked.States;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects
{
  public class CheckGameTurnLimit : EffectBase<CheckGameTurnLimit>
  {
    protected override void ResolveImpl(IMutableState state)
    {
      if (state.TurnNumber < state.Metadata.TurnCountLimit)
        return;

      state.IsGameOver = true;
      state.Effects.Clear();

      if (TryFindPlayerWithMostKeys(state, out var winner))
      {
        state.PlayerTurn = winner;
        return;
      }

      Forge(state);
      if (TryFindPlayerWithMostKeys(state, out winner))
      {
        state.PlayerTurn = winner;
        return;
      }

      if (TryFindPlayerWithMostAember(state, out winner))
      {
        state.PlayerTurn = winner;
        return;
      }

      if (TryFindPlayerWIthMostPotentialAember(state, out winner))
      {
        state.PlayerTurn = winner;
        return;
      }

      state.PlayerTurn = Player.Player1;
    }

    static bool TryFindPlayerWithMostKeys(IState state, out Player player)
    {
      if (state.Keys[Player.Player1] > state.Keys[Player.Player2])
      {
        player = Player.Player1;
        return true;
      }

      if (state.Keys[Player.Player2] > state.Keys[Player.Player1])
      {
        player = Player.Player2;
        return true;
      }

      player = default;
      return false;
    }

    static bool TryFindPlayerWithMostAember(IState state, out Player player)
    {
      if (state.Aember[Player.Player1] > state.Aember[Player.Player2])
      {
        player = Player.Player1;
        return true;
      }

      if (state.Aember[Player.Player2] > state.Aember[Player.Player1])
      {
        player = Player.Player2;
        return true;
      }

      player = default;
      return false;
    }

    static void Forge(IMutableState state)
    {
      Forge(state, Player.Player1);
      Forge(state, Player.Player2);
    }

    static void Forge(IMutableState state, Player player)
    {
      if (state.Aember[player] >= Constants.DefaultForgeCost)
      {
        state.Keys[player]++;
        state.Aember[player] -= Constants.DefaultForgeCost;
      }
    }

    bool TryFindPlayerWIthMostPotentialAember(IState state, out Player player)
    {
      var player1PotentialAember = PotentialAember(state, Player.Player1);
      var player2PotentialAember = PotentialAember(state, Player.Player2);
      if (player1PotentialAember > player2PotentialAember)
      {
        player = Player.Player1;
        return true;
      }

      if (player2PotentialAember > player1PotentialAember)
      {
        player = Player.Player2;
        return true;
      }

      player = default;
      return false;
    }

    int PotentialAember(IState state, Player player)
    {
      var groupedCreatures = state.Fields[player].GroupBy(c => c.Card.House);
      return groupedCreatures.Count() > 0 ? groupedCreatures.Max(g => g.Count()) : 0;
      // TODO include aember bonus on cards in hand
    }
  }
}