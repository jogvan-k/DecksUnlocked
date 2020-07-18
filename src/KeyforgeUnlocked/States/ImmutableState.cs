using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public sealed class ImmutableState : StateBase, IState
  {
    public Player PlayerTurn { get; }

    public int TurnNumber { get; }

    public bool IsGameOver { get; }

    public IState PreviousState { get; }

    public House? ActiveHouse { get; }

    public IImmutableDictionary<Player, int> Keys { get; }

    public IImmutableDictionary<Player, int> Aember { get; }

    public IImmutableList<IActionGroup> ActionGroups { get; }

    public IImmutableDictionary<Player, Stack<Card>> Decks { get; }

    public IImmutableDictionary<Player, ISet<Card>> Hands { get; }

    public IImmutableDictionary<Player, ISet<Card>> Discards { get; }

    public IImmutableDictionary<Player, ISet<Card>> Archives { get; }

    public IImmutableDictionary<Player, IList<Creature>> Fields { get; }

    public ImmutableArray<IEffect> Effects { get; }

    public IImmutableList<IResolvedEffect> ResolvedEffects { get; }

    public Metadata Metadata { get; }

    public ImmutableState(
      Player playerTurn,
      int turnNumber,
      bool isGameOver,
      IState previousState,
      House? activeHouse,
      IImmutableDictionary<Player, int> keys,
      IImmutableDictionary<Player, int> aember,
      IImmutableList<IActionGroup> actionGroups,
      IImmutableDictionary<Player, Stack<Card>> decks,
      IImmutableDictionary<Player, ISet<Card>> hands,
      IImmutableDictionary<Player, ISet<Card>> discards,
      IImmutableDictionary<Player, ISet<Card>> archives,
      IImmutableDictionary<Player, IList<Creature>> fields,
      ImmutableArray<IEffect> effects,
      IImmutableList<IResolvedEffect> resolvedEffects,
      Metadata metadata)
    {
      PlayerTurn = playerTurn;
      TurnNumber = turnNumber;
      IsGameOver = isGameOver;
      PreviousState = previousState;
      ActiveHouse = activeHouse;
      Keys = keys;
      Aember = aember;
      ActionGroups = actionGroups;
      Decks = decks;
      Hands = hands;
      Discards = discards;
      Archives = archives;
      Fields = fields;
      Effects = effects;
      ResolvedEffects = resolvedEffects;
      Metadata = metadata;
    }
  }
}