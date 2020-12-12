using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using Action = KeyforgeUnlocked.Actions.Action;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class DeclareHouseGroup : ActionGroupBase
  {
    public IImmutableSet<House> Houses;

    public DeclareHouseGroup(IEnumerable<House> houses)
    {
      Houses = houses.ToImmutableHashSet();
    }

    protected override IImmutableList<Action> InitiateActions(ImmutableState origin)
    {
      return Houses.Select(h => new DeclareHouse(origin, h)).ToImmutableList<Action>();
    }

    bool Equals(DeclareHouseGroup other)
    {
      return base.Equals(other) && Houses.SetEquals(other.Houses);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((DeclareHouseGroup) obj);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Houses);
    }
  }
}