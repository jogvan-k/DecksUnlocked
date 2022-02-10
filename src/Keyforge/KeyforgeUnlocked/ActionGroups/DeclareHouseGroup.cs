using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
    public sealed class DeclareHouseGroup : ActionGroupBase<DeclareHouseGroup>
    {
        public IImmutableSet<House> Houses;

        public DeclareHouseGroup(IEnumerable<House> houses)
        {
            Houses = houses.ToImmutableHashSet();
        }

        protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
        {
            return Houses.Select(h => new DeclareHouse(origin, h)).ToImmutableList<IAction>();
        }

        protected override bool Equals(DeclareHouseGroup other)
        {
            return Houses.SetEquals(other.Houses);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Houses);
        }

        public override string ToString()
        {
            return $"Declare house";
        }
    }
}