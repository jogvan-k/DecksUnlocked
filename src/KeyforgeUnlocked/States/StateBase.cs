using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Types;
using Microsoft.FSharp.Core;
using UnlockedCore;

namespace KeyforgeUnlocked.States
{
  public abstract class StateBase
  {
    public ICoreAction[] Actions()
    {
      var origin = ToImmutable();
      return ((IState) this).ActionGroups
        .SelectMany(a => a.Actions(origin))
        .OrderBy(a => a.Identity())
        .ToArray();
    }

    protected internal IState _previousState;

    public FSharpOption<ICoreState> PreviousState => _previousState != null
      ? FSharpOption<ICoreState>.Some(_previousState)
      : FSharpOption<ICoreState>.None;

    /// <summary>
    /// Creates a mutable instance of <see cref="IState"/>. All properties are cloned except of <see cref="Previousstate"/> which is set to the state initiating the mutable state, and the <see cref="resolvedEffects"/>  is emptied.
    /// </summary>
    public MutableState ToMutable()
    {
      return new((IState) this);
    }

    public ImmutableState ToImmutable()
    {
      if (this is ImmutableState s)
        return s;
      return new ImmutableState((IState) this);
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
             && thisState.ActiveHouse == other.ActiveHouse
             && EqualValues(thisState.Keys, other.Keys)
             && EqualValues(thisState.Aember, other.Aember)
             && EqualityComparer.Equals(thisState.ActionGroups, other.ActionGroups)
             && EqualityComparer.Equals(thisState.Decks, other.Decks)
             && EqualityComparer.Equals(thisState.Hands, other.Hands)
             && EqualityComparer.Equals(thisState.Discards, other.Discards)
             && EqualityComparer.Equals(thisState.Archives, other.Archives)
             && EqualityComparer.Equals(thisState.Fields, other.Fields)
             && EqualityComparer.Equals(thisState.Artifacts, other.Artifacts)
             && thisState.Effects.SequenceEqual(other.Effects)
             && thisState.ResolvedEffects.SequenceEqual(other.ResolvedEffects)
             && thisState.HistoricData.Equals(other.HistoricData)
             && ReferenceEquals(thisState.Metadata, other.Metadata);
    }

    static bool EqualValues<T>(IReadOnlyDictionary<Player, T> first,
      IReadOnlyDictionary<Player, T> second) where T : struct
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
      hashCode.Add(EqualityComparer.GetHashCode(thisState.ActionGroups));
      hashCode.Add(thisState.Keys);
      hashCode.Add(thisState.Aember);
      hashCode.Add(EqualityComparer.GetHashCode(thisState.Decks));
      hashCode.Add(EqualityComparer.GetHashCode(thisState.Hands));
      hashCode.Add(EqualityComparer.GetHashCode(thisState.Discards));
      hashCode.Add(EqualityComparer.GetHashCode(thisState.Archives));
      hashCode.Add(EqualityComparer.GetHashCode(thisState.Fields));
      hashCode.Add(EqualityComparer.GetHashCode(thisState.Artifacts));
      hashCode.Add(EqualityComparer.GetHashCode(thisState.Effects));
      hashCode.Add(thisState.HistoricData);
      hashCode.Add(thisState.Metadata);
      return hashCode.ToHashCode();
    }
  }
}