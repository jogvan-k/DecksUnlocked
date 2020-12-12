using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  sealed class DeclareHouseTest : ActionTestBase<DeclareHouse>
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
      var sut = new DeclareHouse(null, House.Mars);

      System.Action<NoMetadataException> asserts = e => { };

      ActExpectException(sut, state, asserts);
    }

    [Test]
    public void Act_HouseNotAvailable_ThrowException()
    {
      var metadata = new Metadata(null, AvailableHouses, 40);
      var state = StateTestUtil.EmptyState.New(metadata: metadata);
      var sut = new DeclareHouse(null, House.Brobnar);

      System.Action<DeclaredHouseNotAvailableException> asserts = e =>
      {
        Assert.AreEqual(state, e.State);
        Assert.AreEqual(House.Brobnar, e.House);
      };

      ActExpectException(sut, state, asserts);
    }

    [Test]
    public void Act_HouseAvailable()
    {
      var metadata = new Metadata(null, AvailableHouses, 40);
      var state = StateTestUtil.EmptyState.New(metadata: metadata);
      var declaredHouse = House.Shadows;
      var sut = new DeclareHouse(null, declaredHouse);

      var expectedResolvedEffects = new LazyList<IResolvedEffect> {new HouseDeclared(declaredHouse)};
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: declaredHouse, metadata: metadata, resolvedEffects: expectedResolvedEffects);
      
      Act(sut, state, expectedState);
    }
  }
}