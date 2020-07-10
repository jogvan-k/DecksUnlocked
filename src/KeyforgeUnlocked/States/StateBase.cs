using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.ActionGroup;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.States
{
  public abstract class StateBase : IState
  {
    protected Player _playerTurn;
    protected int _turnNumber;
    protected bool _isGameOver;
    protected List<IActionGroup> _actionGroups;
    protected Dictionary<Player, Stack<Card>> _decks;
    protected Dictionary<Player, ISet<Card>> _hands;
    protected Dictionary<Player, ISet<Card>> _discards;
    protected Dictionary<Player, ISet<Card>> _archives;
    protected Dictionary<Player, IList<Creature>> _fields;
    protected Queue<Effect> _effects;

    public Player PlayerTurn => _playerTurn;

    public int TurnNumber => _turnNumber;

    public bool IsGameOver => _isGameOver;

    public List<IActionGroup> ActionGroups => _actionGroups;

    public Dictionary<Player, Stack<Card>> Decks => _decks;

    public Dictionary<Player, ISet<Card>> Hands => _hands;

    public Dictionary<Player, ISet<Card>> Discards => _discards;

    public Dictionary<Player, ISet<Card>> Archives => _archives;

    public Dictionary<Player, IList<Creature>> Fields => _fields;

    public Queue<Effect> Effects => _effects;

    public StateBase(Player playerTurn,
      int turnNumber,
      Dictionary<Player, Stack<Card>> decks,
      Dictionary<Player, ISet<Card>> hands,
      Dictionary<Player, ISet<Card>> discards,
      Dictionary<Player, ISet<Card>> archives,
      Dictionary<Player, IList<Creature>> fields,
      Queue<Effect> effects,
      List<IActionGroup> actionGroups)
    {
      _playerTurn = playerTurn;
      _turnNumber = turnNumber;
      _decks = decks;
      _hands = hands;
      _discards = discards;
      _archives = archives;
      _fields = fields;
      _effects = effects;
      _actionGroups = actionGroups;
    }

    public List<CoreAction> Actions()
    {
      return ActionGroups.SelectMany(a => a.Actions).Cast<CoreAction>().ToList();
    }

    public Immutable ToImmutable()
    {
      return new Immutable(
        PlayerTurn,
        TurnNumber,
        Decks,
        Hands,
        Discards,
        Archives,
        Fields,
        Effects,
        ActionGroups);
    }

    public MutableState ToMutable()
    {
      // TODO clone fields
      return new MutableState(
        PlayerTurn,
        TurnNumber,
        Decks,
        Hands,
        Discards,
        Archives,
        Fields,
        Effects,
        ActionGroups);
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
      return IsGameOver == other.IsGameOver
             && TurnNumber == other.TurnNumber
             && PlayerTurn == other.PlayerTurn
             && EqualContent(Decks, other.Decks)
             && EqualContent(Hands, other.Hands)
             && EqualContent(Discards, other.Discards)
             && EqualContent(Archives, other.Archives)
             && EqualContent(Fields, other.Fields)
             && Effects.SequenceEqual(other.Effects);
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
      hashCode.Add(IsGameOver);
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