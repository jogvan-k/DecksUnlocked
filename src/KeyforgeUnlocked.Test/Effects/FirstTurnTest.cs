using System.Collections.Generic;
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
    readonly CreatureCard sampleCard1 = new SimpleCreatureCard();
    readonly CreatureCard sampleCard2 = new SimpleCreatureCard();


    [Test]
    public void Resolve_EmptyState()
    {
      var hands = new Dictionary<Player, ISet<Card>>
      {
        {Player.Player1, new HashSet<Card> {sampleCard1, sampleCard2}},
        {Player.Player2, new HashSet<Card>()}
      };
      var state = StateTestUtil.EmptyMutableState.New(hands: hands);
      var sut = new FirstTurn();

      sut.Resolve(state);

      var expectedActionGroups = new List<IActionGroup>
      {
        new PlayCreatureCardGroup(state, sampleCard1), new PlayCreatureCardGroup(state, sampleCard2), new NoActionGroup()
      };
      var expectedState = StateTestUtil.EmptyState.New(actionGroups: expectedActionGroups, hands: hands);
      Assert.AreEqual(expectedState, state);
    }
  }
}