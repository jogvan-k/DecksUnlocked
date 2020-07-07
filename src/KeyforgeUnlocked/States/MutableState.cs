using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  // TODO Refactor to popsicle immutable?
  public class MutableState : State
  {
    public bool IsGameOver { get; set; }

    public List<CoreAction> Actions { get; private set; }

    public Player PlayerTurn { get; set; }

    public int TurnNumber { get; set; }

    public Dictionary<Player, Stack<Card>> Decks { get; set; }

    public Dictionary<Player, ISet<Card>> Hands { get; set; }

    public Dictionary<Player, ISet<Card>> Discards { get; set; }

    public Dictionary<Player, ISet<Card>> Archives { get; set; }

    public Dictionary<Player, IList<Creature>> Fields { get; set; }

    public Queue<Effect> Effects { get; set; }

    public MutableState(
      Player playerTurn,
      int turnNumber,
      Dictionary<Player, Stack<Card>> decks,
      Dictionary<Player, ISet<Card>> hands,
      Dictionary<Player, ISet<Card>> discards,
      Dictionary<Player, ISet<Card>> archives,
      Dictionary<Player, IList<Creature>> fields,
      Queue<Effect> effects)
    {
      PlayerTurn = playerTurn;
      TurnNumber = turnNumber;
      Decks = decks;
      Hands = hands;
      Discards = discards;
      Archives = archives;
      Fields = fields;
      Effects = effects;
      Actions = new List<CoreAction>();
    }

    ImmutableState Immutable()
    {
      return new ImmutableState(
        PlayerTurn,
        TurnNumber,
        Actions,
        Decks,
        Hands,
        Discards,
        Archives,
        Fields,
        Effects);
    }

    void RefreshBaseActions()
    {
      Actions = IsGameOver ? new List<CoreAction>() : new List<CoreAction>() {new EndTurn(this)};
    }

    public ImmutableState ResolveEffects()
    {
      while (Actions.Count == 0 && Effects.Count != 0)
        Effects.Dequeue().Resolve(this);

      if (Actions.Count == 0)
        RefreshBaseActions();

      return this.Immutable();
    }

    public override bool Equals(object? obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((MutableState) obj);
    }

    bool Equals(MutableState other)
    {
      return IsGameOver == other.IsGameOver
             && TurnNumber == other.TurnNumber
             && PlayerTurn == other.PlayerTurn
             && Actions.SequenceEqual(other.Actions)
             && EqualContent(Decks, other.Decks)
             && EqualContent(Hands, other.Hands)
             && EqualContent(Discards, other.Discards)
             && EqualContent(Archives, other.Archives)
             && EqualContent(Fields, other.Fields)
             && Effects.SequenceEqual(other.Effects);
    }

    static bool EqualContent<T>(IDictionary<Player, T> first,
      IDictionary<Player, T> second) where T: IEnumerable<object>
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
      hashCode.Add(IsGameOver);
      hashCode.Add(Actions);
      hashCode.Add((int) PlayerTurn);
      hashCode.Add(TurnNumber);
      hashCode.Add(Decks);
      hashCode.Add(Hands);
      hashCode.Add(Discards);
      hashCode.Add(Archives);
      hashCode.Add(Fields);
      hashCode.Add(Effects);
      return hashCode.ToHashCode();
    }
  }
}