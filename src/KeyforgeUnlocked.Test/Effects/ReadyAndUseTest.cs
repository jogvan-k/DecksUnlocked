using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  sealed class ReadyAndUseTest
  {
    CreatureCard sampleCreatureCard = new SampleCreatureCard();

    [Test]
    public void Resolve_TargetCreatureDoesNotBelongToCurrentPlayer_Exception()
    {
      var target = new Creature(sampleCreatureCard);
      var fields = TestUtil.Lists(target);
      var state = StateTestUtil.EmptyState.New(Player.Player2, fields: fields);

      var sut = new ReadyAndUse(target);

      try
      {
        sut.Resolve(state);
      }
      catch (InvalidTargetException e)
      {
        Assert.AreEqual(state, e.State);
        Assert.AreEqual(sampleCreatureCard.Id, e.TargetId);
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void Resolve()
    {
      var target = new Creature(sampleCreatureCard);
      var fields = TestUtil.Lists(target);
      var state = StateTestUtil.EmptyState.New(fields: fields);
      var sut = new ReadyAndUse(target);

      sut.Resolve(state);

      var readiedCreature = new Creature(sampleCreatureCard, isReady: true);
      var expectedFields = TestUtil.Lists(readiedCreature);
      var expectedResolvedEffects = new List<IResolvedEffect> {new CreatureReadied(readiedCreature)};
      var expectedActionGroups = new List<IActionGroup> {new UseCreatureGroup(state, readiedCreature)};
      var expectedState = StateTestUtil.EmptyState.New(
        fields: expectedFields, resolvedEffects: expectedResolvedEffects, actionGroups: expectedActionGroups);
    }
  }
}