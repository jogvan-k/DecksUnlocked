using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public abstract class StateBase
  {
    protected Player playerTurn;
    protected int turnNumber;
    protected bool isGameOver;
    protected IList<IActionGroup> actionGroups;
    protected IDictionary<Player, Stack<Card>> decks;
    protected IDictionary<Player, ISet<Card>> hands;
    protected IDictionary<Player, ISet<Card>> discards;
    protected IDictionary<Player, ISet<Card>> archives;
    protected IDictionary<Player, IList<Creature>> fields;
    protected Queue<IEffect> effects;

    public StateBase(Player playerTurn,
      int turnNumber,
      IDictionary<Player, Stack<Card>> decks,
      IDictionary<Player, ISet<Card>> hands,
      IDictionary<Player, ISet<Card>> discards,
      IDictionary<Player, ISet<Card>> archives,
      IDictionary<Player, IList<Creature>> fields,
      Queue<IEffect> effects,
      IList<IActionGroup> actionGroups)
    {
      this.playerTurn = playerTurn;
      this.turnNumber = turnNumber;
      this.decks = decks;
      this.hands = hands;
      this.discards = discards;
      this.archives = archives;
      this.fields = fields;
      this.effects = effects;
      this.actionGroups = actionGroups;
    }

    public IList<ICoreAction> Actions()
    {
      return actionGroups.SelectMany(a => a.Actions).Cast<ICoreAction>().ToList();
    }

    public ImmutableState ToImmutable()
    {
      return new ImmutableState(
        playerTurn,
        turnNumber,
        decks,
        hands,
        discards,
        archives,
        fields,
        effects,
        actionGroups);
    }

    public MutableState ToMutable()
    {
      // TODO clone fields
      return new MutableState(
        playerTurn,
        turnNumber,
        decks,
        hands,
        discards,
        archives,
        fields,
        effects,
        actionGroups);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((MutableState) obj);
    }

    bool Equals(MutableState other)
    {
      return isGameOver == other.IsGameOver
             && turnNumber == other.TurnNumber
             && playerTurn == other.PlayerTurn
             && EqualContent(decks, other.Decks)
             && EqualContent(hands, other.Hands)
             && EqualContent(discards, other.Discards)
             && EqualContent(archives, other.Archives)
             && EqualContent(fields, other.Fields)
             && effects.SequenceEqual(other.Effects);
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
      return hashCode.ToHashCode();
    }
  }
}