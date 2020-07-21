using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Creatures;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public abstract class StateBase
  {
    public IList<ICoreAction> Actions()
    {
      return ((IState) this).ActionGroups.SelectMany(a => a.Actions).Cast<ICoreAction>().ToList();
    }


    /// <summary>
    /// Creates a mutable instance of <see cref="IState"/>. All properties are cloned except of <see cref="Previousstate"/> which is set to the state initiating the mutable state, and the <see cref="resolvedEffects"/>  is emptied.
    /// </summary>
    public MutableState ToMutable()
    {
      return new MutableState((IState) this);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (!(obj is IState)) return false;
      return Equals((IState) obj);
    }

    bool Equals(IState other)
    {
      var thisState = (IState) this;
      return thisState.IsGameOver == other.IsGameOver
             && thisState.TurnNumber == other.TurnNumber
             && thisState.PlayerTurn == other.PlayerTurn
             && (thisState.PreviousState == null && other.PreviousState == null ||
                 (thisState.PreviousState != null && thisState.PreviousState.Equals(other.PreviousState)))
             && thisState.ActiveHouse == other.ActiveHouse
             && EqualValues(thisState.Keys, other.Keys)
             && EqualValues(thisState.Aember, other.Aember)
             && thisState.ActionGroups.SequenceEqual(other.ActionGroups)
             && EqualContent(thisState.Decks, other.Decks)
             && SetEquals(thisState.Hands, other.Hands)
             && SetEquals(thisState.Discards, other.Discards)
             && SetEquals(thisState.Archives, other.Archives)
             && EqualContent(thisState.Fields, other.Fields)
             && thisState.Effects.SequenceEqual(other.Effects)
             && thisState.ResolvedEffects.SequenceEqual(other.ResolvedEffects)
             && ReferenceEquals(thisState.Metadata, other.Metadata);
    }

    static bool SetEquals<T>(IImmutableDictionary<Player, ISet<T>> first,
      IImmutableDictionary<Player, ISet<T>> second)
    {
      if (first.Count != second.Count)
        return false;
      foreach (var key in first.Keys)
      {
        if (!second.ContainsKey(key) || !first[key].SetEquals(second[key]))
          return false;
      }

      return true;
    }

    static bool EqualContent<T>(IImmutableDictionary<Player, T> first,
      IImmutableDictionary<Player, T> second) where T : IEnumerable<object>
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

    // reduce and incorporate to above function?
    static bool EqualContent(IImmutableDictionary<Player, IList<Creature>> first,
      IImmutableDictionary<Player, IList<Creature>> second)
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

    static bool EqualValues<T>(IImmutableDictionary<Player, T> first,
      IImmutableDictionary<Player, T> second) where T : struct
    {
      if (first.Count != second.Count)
        return false;
      foreach (var key in first.Keys)
      {
        if (!second.ContainsKey(key) || !first[key].Equals(second[key]))
          return false;
      }

      return true;
    }

    public override int GetHashCode()
    {
      var thisState = (IState) this;
      var hashCode = new HashCode();
      hashCode.Add(thisState.PlayerTurn);
      hashCode.Add(thisState.TurnNumber);
      hashCode.Add(thisState.IsGameOver);
      hashCode.Add(thisState.ActiveHouse);
      hashCode.Add(thisState.Keys);
      hashCode.Add(thisState.Aember);
      hashCode.Add(thisState.Decks);
      hashCode.Add(thisState.Hands);
      hashCode.Add(thisState.Decks);
      hashCode.Add(thisState.Archives);
      hashCode.Add(thisState.Fields);
      hashCode.Add(thisState.Effects);
      hashCode.Add(thisState.ResolvedEffects);
      hashCode.Add(thisState.Metadata);
      return hashCode.ToHashCode();
    }
  }
}