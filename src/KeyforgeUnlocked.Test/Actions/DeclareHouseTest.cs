using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  sealed class DeclareHouseTest : ActionTestBase
  {
    static readonly IImmutableSet<House> Player1AvailableHouses =
      new[] {House.Mars, House.Dis, House.Shadows}.ToImmutableHashSet();

    static readonly IImmutableSet<House> Player2AvailableHouses =
      new[] {House.Sanctum, House.Brobnar, House.StarAlliance}.ToImmutableHashSet();

    static readonly ImmutableDictionary<Player, IImmutableSet<House>> AvailableHouses =
      ImmutableDictionary<Player, IImmutableSet<House>>.Empty.AddRange(
        new[]
        {
          new KeyValuePair<Player, IImmutableSet<House>>(Player.Player1, Player1AvailableHouses),
          new KeyValuePair<Player, IImmutableSet<House>>(Player.Player2, Player2AvailableHouses)
        });

    [Test]
    public void Act_EmptyState_ThrowException()
    {
      var state = StateTestUtil.EmptyState.New();
      var sut = new DeclareHouse(House.Mars);

      try
      {
        Act(sut, state);
      }
      catch (NoMetadataException e)
      {
        Assert.AreEqual(state, e.State);
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void Act_HouseNotAvailable_ThrowException()
    {
      var metadata = new Metadata(null, AvailableHouses);
      var state = StateTestUtil.EmptyState.New(metadata: metadata);
      var sut = new DeclareHouse(House.Brobnar);

      try
      {
        Act(sut, state);
      }
      catch (DeclaredHouseNotAvailableException e)
      {
        Assert.AreEqual(state, e.State);
        Assert.AreEqual(House.Brobnar, e.House);
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void Act_HouseAvailable()
    {
      var metadata = new Metadata(null, AvailableHouses);
      var state = StateTestUtil.EmptyState.New(metadata: metadata);
      var declaredHouse = House.Shadows;
      var sut = new DeclareHouse(declaredHouse);

      Act(sut, state);

      var expectedResolvedEffects = new List<IResolvedEffect> {new HouseDeclared(declaredHouse)};
      var expectedState = StateTestUtil.EmptyState.New(activeHouse: declaredHouse, metadata: metadata, resolvedEffects: expectedResolvedEffects);
      Assert.AreEqual(expectedState, state);
    }
  }
}