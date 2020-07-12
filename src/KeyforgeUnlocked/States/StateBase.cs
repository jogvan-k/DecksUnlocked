using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public abstract class StateBase
  {
    protected Player playerTurn;
    protected int turnNumber;
    protected bool isGameOver;
    protected IState previousState;
    protected IList<IResolvedEffect> resolvedEffects;
    protected IList<IActionGroup> actionGroups;
    protected IDictionary<Player, Stack<Card>> decks;
    protected IDictionary<Player, ISet<Card>> hands;
    protected IDictionary<Player, ISet<Card>> discards;
    protected IDictionary<Player, ISet<Card>> archives;
    protected IDictionary<Player, IList<Creature>> fields;
    protected Queue<IEffect> effects;

    public StateBase(Player playerTurn,
      int turnNumber,
      bool isGameOver,
      IState previousState,
      IList<IActionGroup> actionGroups,
      IDictionary<Player, Stack<Card>> decks,
      IDictionary<Player, ISet<Card>> hands,
      IDictionary<Player, ISet<Card>> discards,
      IDictionary<Player, ISet<Card>> archives,
      IDictionary<Player, IList<Creature>> fields,
      Queue<IEffect> effects,
      IList<IResolvedEffect> resolvedEffects)
    {
      this.playerTurn = playerTurn;
      this.turnNumber = turnNumber;
      this.isGameOver = isGameOver;
      this.previousState = previousState;
      this.actionGroups = actionGroups;
      this.decks = decks;
      this.hands = hands;
      this.discards = discards;
      this.archives = archives;
      this.fields = fields;
      this.effects = effects;
      this.resolvedEffects = resolvedEffects;
    }

    public IList<ICoreAction> Actions()
    {
      return actionGroups.SelectMany(a => a.Actions).Cast<ICoreAction>().ToList();
    }

    protected ImmutableState ToImmutable()
    {
      return new ImmutableState(
        playerTurn,
        turnNumber,
        isGameOver,
        previousState,
        actionGroups,
        decks,
        hands,
        discards,
        archives,
        fields,
        effects,
        new List<IResolvedEffect>());
    }

    /// <summary>
    /// Creates a mutable instance of <see cref="IState"/>. All properties are cloned except of <see cref="previousState"/> which is set to the state initiating the mutable state, and the <see cref="resolvedEffects"/>  is emptied.
    /// </summary>
    public MutableState ToMutable()
    {
      // TODO clone fields
      return new MutableState(
        playerTurn,
        turnNumber,
        isGameOver,
        (IState) this,
        actionGroups,
        decks,
        hands,
        discards,
        archives,
        fields,
        effects,
        new List<IResolvedEffect>());
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (!(obj is StateBase)) return false;
      return Equals((StateBase) obj);
    }

    bool Equals(StateBase other)
    {
      return isGameOver == other.isGameOver
             && turnNumber == other.turnNumber
             && playerTurn == other.playerTurn
             && (previousState == null && other.previousState == null ||
                 (previousState != null && previousState.Equals(other.previousState)))
             && actionGroups.SequenceEqual(other.actionGroups)
             && EqualContent(decks, other.decks)
             && EqualContent(hands, other.hands)
             && EqualContent(discards, other.discards)
             && EqualContent(archives, other.archives)
             && EqualContent(fields, other.fields)
             && effects.SequenceEqual(other.effects)
             && resolvedEffects.SequenceEqual(other.resolvedEffects);
    }

    static bool EqualContent<T>(IDictionary<Player, T> first,
      IDictionary<Player, T> second) where T : IEnumerable<object>
    {
      if (first.Count != second.Count)
        return false;
      foreach (var key in first.Keys)
      {
        if (!second.ContainsKey(key) || !first[key].SequenceEqual(second[key]))
          return false;
      }

      return true;
    }

    public override int GetHashCode()
    {
      var hashCode = new HashCode();
      hashCode.Add(isGameOver);
      hashCode.Add((int) playerTurn);
      hashCode.Add(playerTurn);
      hashCode.Add(decks);
      hashCode.Add(hands);
      hashCode.Add(decks);
      hashCode.Add(archives);
      hashCode.Add(fields);
      hashCode.Add(effects);
      hashCode.Add(resolvedEffects);
      return hashCode.ToHashCode();
    }
  }
}