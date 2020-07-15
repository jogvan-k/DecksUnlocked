using System.Collections.Generic;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;
using Reap = KeyforgeUnlocked.Actions.Reap;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class ReapTest : ActionTestBase
  {
    readonly string CreatureId = "creatureId";

    [Test]
    public void Resolve_EmptyBoard_CreatureNotFoundException()
    {
      var state = StateUtil.EmptyMutableState;
      var sut = new Reap(CreatureId);

      try
      {
        Act(sut, state);
      }
      catch (CreatureNotPresentException e)
      {
        Assert.AreEqual(CreatureId, e.CreatureId);
        Assert.AreSame(state, e.State);
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void Resolve_CreatureNotReady_CreatureNotReadyException()
    {
      var fields = new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature> {CreatureTestUtil.SampleCreature(CreatureId, false)}},
        {Player.Player2, new List<Creature>()}
      };
      var state = StateUtil.EmptyState.New(fields: fields);
      var sut = new Reap(CreatureId);

      try
      {
        Act(sut, state);
      }
      catch (CreatureNotReadyException e)
      {
        Assert.AreEqual(CreatureTestUtil.SampleCreature(CreatureId, false), e.Creature);
        Assert.AreSame(state, e.State);
      }
    }

    [Test]
    public void Act_CreatureReady()
    {
      var fields = new Dictionary<Player, IList<Creature>>
      {
        {Player.Player1, new List<Creature> {CreatureTestUtil.SampleCreature(CreatureId, true)}},
        {Player.Player2, new List<Creature>()}
      };
      var state = StateUtil.EmptyState.New(fields: fields);
      var sut = new Reap(CreatureId);

      Act(sut, state);

      var expectedEffects = new Queue<IEffect>();
      expectedEffects.Enqueue(new KeyforgeUnlocked.Effects.Reap(CreatureId));
      var expectedState = StateUtil.EmptyState.New(fields: fields, effects: expectedEffects);
      Assert.AreEqual(expectedState, state);
    }
  }
}