using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class FirstTurnTest
  {
    readonly CreatureCard[] LogosCreatureCards = {new LogosCreatureCard(), new LogosCreatureCard()};

    readonly CreatureCard[] StarAllianceCreatureCards =
      {new StarAllianceCreatureCard(), new StarAllianceCreatureCard()};

    readonly CreatureCard[] UntamedCreatureCards = {new UntamedCreatureCard(), new UntamedCreatureCard()};


    [Test]
    public void Resolve_DifferentHouses_OnlyActionGroupsOnActiveHouse()
    {
      var activeHouse = House.Logos;
      var hands = new Dictionary<Player, ISet<Card>>
      {
        {
          Player.Player1,
          new HashSet<Card>(LogosCreatureCards.Concat(StarAllianceCreatureCards).Concat(UntamedCreatureCards))
        },
        {Player.Player2, new HashSet<Card>()}
      };
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
        activeHouse: activeHouse, actionGroups: expectedActionGroups, hands: hands);
      Assert.AreEqual(expectedState, state);
    }

    [Test]
    public void Resolve_NoCardOfActiveHouse_OnlyNoActionGroup()
    {
      var activeHouse = House.Brobnar;
      var hands = new Dictionary<Player, ISet<Card>>
      {
        {Player.Player1, new HashSet<Card> {new StarAllianceCreatureCard()}},
        {Player.Player2, new HashSet<Card>()}
      };
      var state = StateTestUtil.EmptyMutableState.New(activeHouse: activeHouse, hands: hands);
      var sut = new FirstTurn();

      sut.Resolve(state);

      var expectedActionGroups = new List<IActionGroup> {new NoActionGroup()};
      var expectedState = StateTestUtil.EmptyState.New(
        activeHouse: activeHouse, actionGroups: expectedActionGroups, hands: hands);
      Assert.AreEqual(expectedState, state);
    }
  }
}