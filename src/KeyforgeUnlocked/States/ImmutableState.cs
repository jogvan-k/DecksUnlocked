using System.Collections.Generic;
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
    public Player PlayerTurn => playerTurn;

    public int TurnNumber => turnNumber;

    public bool IsGameOver => isGameOver;

    public IState PreviousState => previousState;

    public House? ActiveHouse => activeHouse;

    public IDictionary<Player, int> Keys => keys;

    public IDictionary<Player, int> Aember => aember;

    public IList<IActionGroup> ActionGroups => actionGroups;

    public IDictionary<Player, Stack<Card>> Decks => decks;

    public IDictionary<Player, ISet<Card>> Hands => hands;

    public IDictionary<Player, ISet<Card>> Discards => discards;

    public IDictionary<Player, ISet<Card>> Archives => archives;

    public IDictionary<Player, IList<Creature>> Fields => fields;

    public StackQueue<IEffect> Effects => effects;

    public IList<IResolvedEffect> ResolvedEffects => resolvedEffects;
    
    public Metadata Metadata => metadata;

    public ImmutableState(Player playerTurn,
      int turnNumber,
      bool isGameOver,
      IState previousState,
      House? activeHouse,
      IDictionary<Player, int> keys,
      IDictionary<Player, int> aember,
      IList<IActionGroup> actionGroups,
      IDictionary<Player, Stack<Card>> decks,
      IDictionary<Player, ISet<Card>> hands,
      IDictionary<Player, ISet<Card>> discards,
      IDictionary<Player, ISet<Card>> archives,
      IDictionary<Player, IList<Creature>> fields,
      StackQueue<IEffect> effects,
      IList<IResolvedEffect> resolvedEffects,
      Metadata metadata)
      : base(
        playerTurn,
        turnNumber,
        isGameOver,
        previousState,
        activeHouse,
        keys,
        aember,
        actionGroups,
        decks,
        hands,
        discards,
        archives,
        fields,
        effects,
        resolvedEffects,
        metadata)
    {
    }
  }
}