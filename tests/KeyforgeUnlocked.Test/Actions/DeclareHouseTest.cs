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
      state.Metadata = null!;
      var sut = new DeclareHouse(null!, House.Mars);

      System.Action<NoMetadataException> asserts = _ => { };

      ActExpectException(sut, state, asserts);
    }

    [Test]
    public void Act_HouseNotAvailable_ThrowException()
    {
      var metadata = new Metadata(ImmutableDictionary<Player, Deck>.Empty, AvailableHouses, 40,0 );
      var state = StateTestUtil.EmptyState.New(metadata: metadata);
      var sut = new DeclareHouse(StateTestUtil.EmptyState, House.Brobnar);

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
      var metadata = new Metadata(ImmutableDictionary<Player, Deck>.Empty, AvailableHouses, 40, 0);
      var state = StateTestUtil.EmptyState.New(metadata: metadata);
      var declaredHouse = House.Shadows;
      var sut = new DeclareHouse(StateTestUtil.EmptyState, declaredHouse);

      var expectedResolvedEffects = new LazyList<IResolvedEffect> {new HouseDeclared(declaredHouse)};
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: declaredHouse, metadata: metadata, resolvedEffects: expectedResolvedEffects);
      
      ActAndAssert(sut, state, expectedState);
    }
  }
}