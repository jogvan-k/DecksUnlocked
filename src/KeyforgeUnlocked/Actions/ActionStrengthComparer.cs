using System.Collections.Generic;

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
        PlayActionCard _ => Priority.Good,
        PlayCreatureCard _ => Priority.Good,
        EndTurn _ => Priority.Neutral,
        TargetAction _ => Priority.Neutral,
        UseCreatureAbility _ => Priority.Good,
        _ => Priority.Neutral
      };
    }
  }
}