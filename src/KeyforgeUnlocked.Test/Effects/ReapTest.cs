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
    static readonly CreatureCard CreatureCard = new SampleCreatureCard();
    static readonly CreatureCard OtherCreatureCard1 = new SampleCreatureCard();
    static readonly CreatureCard OtherCreatureCard2 = new SampleCreatureCard();
    static readonly Creature creature = new Creature(CreatureCard, isReady: true);
    readonly Reap sut = new Reap(creature);

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
        Assert.AreEqual(creature.Id, e.CreatureId);
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
          {
            Player.Player1,
            new List<Creature> {new Creature(CreatureCard)}
          },
          {Player.Player2, new List<Creature>()}
        });
      var sut = new Reap(new Creature(CreatureCard));

      try
      {
        sut.Resolve(state);
      }
      catch (CreatureNotReadyException e)
      {
        Assert.AreEqual(new Creature(CreatureCard), e.Creature);
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
            new List<Creature>
            {
              new Creature(CreatureCard, isReady: true),
              new Creature(OtherCreatureCard1, isReady: true)
            }
          },
          {
            Player.Player2,
            new List<Creature>
            {
              new Creature(OtherCreatureCard2, isReady: true)
            }
          }
        });

      sut.Resolve(state);

      var expectedField = new Dictionary<Player, IList<Creature>>
      {
        {
          Player.Player1,
          new List<Creature>
          {
            new Creature(CreatureCard),
            new Creature(OtherCreatureCard1, isReady: true)
          }
        },
        {
          Player.Player2,
          new List<Creature>
          {
            new Creature(OtherCreatureCard2, isReady: true)
          }
        }
      };
      var expectedResolvedEffects = new List<IResolvedEffect> {new Reaped(creature)};
      var expectedAember = new Dictionary<Player, int> {{Player.Player1, 1}, {Player.Player2, 0}};
      var expectedState = StateTestUtil.EmptyMutableState.New(
        fields: expectedField, resolvedEffects: expectedResolvedEffects, aember: expectedAember);

      StateAsserter.StateEquals(expectedState, state);
    }
  }
}