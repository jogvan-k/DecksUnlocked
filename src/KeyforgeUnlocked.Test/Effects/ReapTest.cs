using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
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
  class ReapTest
  {
    static string _creatureId = "creatureId";
    static string _otherCreatureId1 = "otherCreatureId1";
    static string _otherCreatureId2 = "otherCreatureId2";
    static CreatureCard _creatureCard = new LogosCreatureCard();
    readonly Reap sut = new Reap(_creatureId);

    [Test]
    public void Resolve_EmptyBoard_CreatureNotPresentException()
    {
      var state = StateTestUtil.EmptyMutableState;

      try
      {
        sut.Resolve(state);
      }
      catch (CreatureNotPresentException e)
      {
        Assert.AreEqual(_creatureId, e.CreatureId);
        Assert.AreSame(state, e.State);
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void Resolve_CreatureNotReady_CreatureNotReadyException()
    {
      var state = StateTestUtil.EmptyState.New(
        fields: new Dictionary<Player, IList<Creature>>
        {
          {Player.Player1, new List<Creature> {CreatureTestUtil.SampleLogosCreature(_creatureId, false)}},
          {Player.Player2, new List<Creature>()}
        });
      var sut = new Reap(_creatureId);

      try
      {
        sut.Resolve(state);
      }
      catch (CreatureNotReadyException e)
      {
        Assert.AreEqual(CreatureTestUtil.SampleLogosCreature(_creatureId, false), e.Creature);
        Assert.AreSame(state, e.State);
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void Resolve_FieldWithCreatures()
    {
      var state = StateTestUtil.EmptyState.New(
        fields: new Dictionary<Player, IList<Creature>>
        {
          {
            Player.Player1,
            new List<Creature> {CreatureTestUtil.SampleLogosCreature(_creatureId, true), CreatureTestUtil.SampleLogosCreature(_otherCreatureId1, true)}
          },
          {Player.Player2, new List<Creature> {CreatureTestUtil.SampleLogosCreature(_otherCreatureId2, true)}}
        });

      sut.Resolve(state);

      var expectedField = new Dictionary<Player, IList<Creature>>
      {
        {
          Player.Player1,
          new List<Creature> {CreatureTestUtil.SampleLogosCreature(_creatureId, false), CreatureTestUtil.SampleLogosCreature(_otherCreatureId1, true)}
        },
        {Player.Player2, new List<Creature> {CreatureTestUtil.SampleLogosCreature(_otherCreatureId2, true)}}
      };
      var expectedResolvedEffects = new List<IResolvedEffect> {new Reaped(CreatureTestUtil.SampleLogosCreature(_creatureId, false))};
      var expectedAember = new Dictionary<Player, int> {{Player.Player1, 1}, {Player.Player2, 0}};
      var expectedState = StateTestUtil.EmptyMutableState.New(
        fields: expectedField, resolvedEffects: expectedResolvedEffects, aember: expectedAember);

      Assert.AreEqual(expectedState, state);
    }
  }
}