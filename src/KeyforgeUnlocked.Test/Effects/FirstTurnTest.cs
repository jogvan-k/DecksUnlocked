using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class FirstTurnTest
  {
    readonly CreatureCard[] LogosCreatureCards = {new SampleCreatureCard(house: House.Logos), new SampleCreatureCard(house: House.Logos)};

    readonly CreatureCard[] StarAllianceCreatureCards =
      {new SampleCreatureCard(house: House.StarAlliance), new SampleCreatureCard(house: House.StarAlliance)};

    readonly CreatureCard[] UntamedCreatureCards = {new SampleCreatureCard(house: House.Undefined), new SampleCreatureCard(house: House.Undefined)};


    [Test]
    public void Resolve_DifferentHouses_OnlyActionGroupsOnActiveHouse()
    {
      var activeHouse = House.Logos;
      var hands = new Dictionary<Player, IMutableSet<ICard>>
      {
        {
          Player.Player1,
          new LazySet<ICard>(LogosCreatureCards.Concat(StarAllianceCreatureCards).Concat(UntamedCreatureCards))
        },
        {Player.Player2, new LazySet<ICard>()}
      }.ToImmutableDictionary();
      var state = StateTestUtil.EmptyMutableState.New(activeHouse: activeHouse, hands: hands);
      var sut = new FirstTurn();

      sut.Resolve(state);

      var expectedActionGroups = new List<IActionGroup>
      {
        new PlayCreatureCardGroup(state, LogosCreatureCards[0]),
        new PlayCreatureCardGroup(state, LogosCreatureCards[1]),
        new NoActionGroup()
      };
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: activeHouse, actionGroups: new LazyList<IActionGroup>(expectedActionGroups), hands: hands);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void Resolve_NoCardOfActiveHouse_OnlyNoActionGroup()
    {
      var activeHouse = House.Brobnar;
      var hands = new Dictionary<Player, IMutableSet<ICard>>
      {
        {Player.Player1, new LazySet<ICard> {new SampleCreatureCard(house: House.StarAlliance)}},
        {Player.Player2, new LazySet<ICard>()}
      }.ToImmutableDictionary();
      var state = StateTestUtil.EmptyMutableState.New(activeHouse: activeHouse, hands: hands);
      var sut = new FirstTurn();

      sut.Resolve(state);

      var expectedActionGroups = new List<IActionGroup> {new NoActionGroup()};
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: activeHouse, actionGroups: new LazyList<IActionGroup>(expectedActionGroups), hands: hands);
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}