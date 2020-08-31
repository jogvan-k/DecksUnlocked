using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
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
    Action<UnresolvedEffectsException> _asserts = e => { };

    private readonly IState _expected = StateTestUtil.EmptyMutableState.New(
      turnNumber: 2, effects: new StackQueue<IEffect>(new[] {new KeyforgeUnlocked.Effects.EndTurn()})).Extend();

    static IEnumerable<TestCaseData> testCases => new List<TestCaseData>
    {
      new TestCaseData(new PlayCreatureCard(null, new SampleCreatureCard(), 0)),
      new TestCaseData(new EndTurn(null)),
      new TestCaseData(new DiscardCard(null, new SampleCreatureCard())),
      new TestCaseData(new Reap(null, new Creature()))
    };

    [TestCaseSource(nameof(testCases))]
    public void Act_UnresolvedEffects_Fail(BasicAction sut)
    {
      ActExpectException(sut, _expected.ToMutable(), _asserts);
    }
  }
}