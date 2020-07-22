using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;
using DeclareHouse = KeyforgeUnlocked.Effects.DeclareHouse;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class DeclareHouseTest
  {
    static readonly House[] Player1Houses = {House.Brobnar, House.Logos, House.Sanctum};
    static readonly House[] Player2Houses = {House.Dis, House.Mars, House.Saurian};

    static readonly ImmutableDictionary<Player, IImmutableSet<House>> Houses =
      ImmutableDictionary<Player, IImmutableSet<House>>.Empty.AddRange(
        new[]
        {
          new KeyValuePair<Player, IImmutableSet<House>>(Player.Player1, Player1Houses.ToImmutableHashSet()),
          new KeyValuePair<Player, IImmutableSet<House>>(Player.Player2, Player2Houses.ToImmutableHashSet())
        });

    static readonly DeclareHouse Sut = new DeclareHouse();

    [Test]
    public void EmptyState_ThrowException()
    {
      var state = StateTestUtil.EmptyMutableState;

      try
      {
        Sut.Resolve(state);
      }
      catch (NoMetadataException e)
      {
        Assert.AreEqual(state, e.State);
        return;
      }

      Assert.Fail();
    }

    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void StateWithMetadata(Player playerTurn)
    {
      var metadata = new Metadata(null, Houses);
      var state = StateTestUtil.EmptyState.New(playerTurn: playerTurn, metadata: metadata);

      Sut.Resolve(state);

      var expectedHouses = playerTurn == Player.Player1 ? Player1Houses : Player2Houses;
      var expectedActionGroups = new List<IActionGroup> {new DeclareHouseGroup(expectedHouses)};
      var expectedState = StateTestUtil.EmptyState.New(
        playerTurn: playerTurn, actionGroups: expectedActionGroups, metadata: metadata);
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}