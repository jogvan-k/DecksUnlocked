using System.Collections.Generic;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using NUnit.Framework;
using DiscardCard = KeyforgeUnlocked.Actions.DiscardCard;
using PlayCreature = KeyforgeUnlocked.Actions.PlayCreature;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  public class BasicActionTest
  {
    static IEnumerable<TestCaseData> testCases => new List<TestCaseData>
    {
      new TestCaseData(new PlayCreature(new SimpleCreatureCard(), 0)),
      new TestCaseData(new EndTurn()),
      new TestCaseData(new DiscardCard(new SimpleCreatureCard())),
    };

    [TestCaseSource(nameof(testCases))]
    public void DoActionNoResolve_UnresolvedEffects_Fail(BasicAction sut)
    {
      var state = TestUtil.EmptyMutableState.New(effects: new Queue<IEffect>(new List<IEffect> {new ChangePlayer()}));

      try
      {
        sut.DoActionNoResolve(state);
      }
      catch (UnresolvedEffectsException e)
      {
        Assert.AreEqual(state, e.State);
        return;
      }

      Assert.Fail();
    }
  }
}