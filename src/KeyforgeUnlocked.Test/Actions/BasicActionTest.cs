using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using DiscardCard = KeyforgeUnlocked.Actions.DiscardCard;
using EndTurn = KeyforgeUnlocked.Actions.EndTurn;
using PlayCreatureCard = KeyforgeUnlocked.Actions.PlayCreatureCard;
using Reap = KeyforgeUnlocked.Actions.Reap;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  public class BasicActionTest : ActionTestBase
  {
    Action<UnresolvedEffectsException> _asserts;

    static IEnumerable<TestCaseData> testCases => new List<TestCaseData>
    {
      new TestCaseData(new PlayCreatureCard(new SampleCreatureCard(), 0)),
      new TestCaseData(new EndTurn()),
      new TestCaseData(new DiscardCard(new SampleCreatureCard())),
      new TestCaseData(new Reap(new Creature()))
    };

    [TestCaseSource(nameof(testCases))]
    public void Act_UnresolvedEffects_Fail(BasicAction sut)
    {
      var state = StateTestUtil.EmptyMutableState.New(
        turnNumber: 2, effects: new StackQueue<IEffect>(new[] {new KeyforgeUnlocked.Effects.EndTurn()}));

      _asserts = e =>
        Assert.AreEqual(state, e.State);

      ActExpectException(sut, state, _asserts);
    }
  }
}