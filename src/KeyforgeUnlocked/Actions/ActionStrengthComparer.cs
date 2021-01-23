using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  /// <summary>
  /// Sorts a sequence of actions by an estimated best-first approach according to an evaluation defined in this class.
  /// </summary>
  public class ActionStrengthComparer : Comparer<IAction>
  {
    public override int Compare(IAction? fst, IAction? snd)
    {
      if (fst == null && snd == null) return 0;
      if (fst == null) return -1;
      if (snd == null) return 1;
      return new ActionStrengthComparerBuilder(fst, snd)
        .ThenBySpecific()
        .ThenByPriority()
        .ThenByType()
        .ThenByIdentity()
        .ComparedValue;
    }
  }

  class ActionStrengthComparerBuilder
  {
    readonly IAction _first, _second;
    public int ComparedValue;

    public ActionStrengthComparerBuilder(IAction first, IAction second)
    {
      this._first = first;
      this._second = second;
    }

    public ActionStrengthComparerBuilder ThenBySpecific()
    {
      if (ComparedValue != 0)
        return this;

      ComparedValue = CompareSpecific(_first, _second);
      return this;
    }

    static int CompareSpecific(IAction first, IAction second)
    {
      if (first is DeclareHouse h1 && second is DeclareHouse h2)
        return CompareDeclareHouse(h1, h2);
      return 0;
    }

    static int CompareDeclareHouse(DeclareHouse h1, DeclareHouse h2)
    {
      return - (PotentialActions(h1) - PotentialActions(h2));
    }

    static int PotentialActions(DeclareHouse h)
    {
      var actingPlayer = h.Origin.PlayerTurn;
      var state = (IState) h.Origin;
      return state.Hands[actingPlayer].Count(c => c.House == h.House)
             + state.Fields[actingPlayer].Count(c => c.Card.House == h.House);
    }

    public ActionStrengthComparerBuilder ThenByPriority()
    {
      if (ComparedValue != 0)
        return this;

      var fstVal = (int) GetPriority(_first);
      var sndVal = (int) GetPriority(_second);

      ComparedValue = fstVal - sndVal;
      return this;
    }

    public ActionStrengthComparerBuilder ThenByType()
    {
      if (ComparedValue != 0)
        return this;

      var fstVal = _first.GetType().Name;
      var sndVal = _second.GetType().Name;

      ComparedValue = fstVal.CompareTo(sndVal);
      return this;
    }

    public ActionStrengthComparerBuilder ThenByIdentity()
    {
      if (ComparedValue != 0)
        return this;

      var fstVal = _first.Identity();
      var sndVal = _second.Identity();

      ComparedValue = fstVal.CompareTo(sndVal);
      return this;
    }
    
    static Priority GetPriority(IAction action)
    {
      return action switch
      {
        DeclareHouse _ => Priority.Neutral,
        DiscardCard _ => Priority.Bad,
        FightCreature _ => Priority.Good,
        Reap _ => Priority.Best,
        RemoveStun _ => Priority.Good,
        EndTurn _ => Priority.Worst,
        TargetAction _ => Priority.Neutral,
        UseCreatureAbility _ => Priority.Good,
        PlayActionCard _ => Priority.Good,
        PlayCreatureCard _ => Priority.Good,
        UseArtifact _ => Priority.Good,
        PlayArtifactCard => Priority.Good,
        _ => Priority.Neutral
      };
    }
  }
}