using System.Linq;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class ReadyCards : IEffect
  {
    public void Resolve(MutableState state)
    {
      var field = state.Fields[state.PlayerTurn];
      for (int i = 0; i < field.Count; i++)
      {
        var creature = field[i];
        if (!creature.IsReady)
        {
          creature.IsReady = true;
          field[i] = creature;
        }
      }
    }

    bool Equals(ReadyCards other)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is ReadyCards other && Equals(other);
    }

    public override int GetHashCode()
    {
      return typeof(ReadyCards).GetHashCode();
    }
  }
}