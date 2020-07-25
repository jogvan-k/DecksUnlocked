using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.States
{
  [TestFixture]
  sealed class SwapCreaturesTest
  {
    static Creature _playerCreature1 = new Creature(new SampleCreatureCard());
    static Creature _playerCreature2 = new Creature(new SampleCreatureCard());
    static Creature _playerCreature3 = new Creature(new SampleCreatureCard());
    static Creature _opponentCreature = new Creature(new SampleCreatureCard());

    [Test]
    public void SwapCreatures_InvalidCreature_CreatureNotPresentException()
    {
      var state = SetupState();
      var creature = new Creature(new SampleCreatureCard());
      try
      {
        state.SwapCreatures(creature.Id, _playerCreature2.Id);
      }
      catch (CreatureNotPresentException e)
      {
        Assert.AreEqual(creature.Id, e.CreatureId);
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void SwapCreatures_InvalidTargetCreature_CreatureNotPresentException()
    {
      var state = SetupState();
      try
      {
        state.SwapCreatures(_playerCreature1.Id, _opponentCreature.Id);
      }
      catch (CreatureNotPresentException e)
      {
        Assert.AreEqual(_opponentCreature.Id, e.CreatureId);
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void SwapCreatures()
    {
      var state = SetupState();

      state.SwapCreatures(_playerCreature1.Id, _playerCreature3.Id);

      var expectedState = SetupState(_playerCreature3, _playerCreature2, _playerCreature1, _opponentCreature);
      StateAsserter.StateEquals(expectedState, state);
    }

    MutableState SetupState()
    {
      return SetupState(_playerCreature1, _playerCreature2, _playerCreature3, _opponentCreature);
    }

    MutableState SetupState(Creature playerCreature1, Creature playerCreature2, Creature playerCreature3,
      Creature opponentCreature)
    {
      var fields = TestUtil.Lists(
        new[]
        {
          playerCreature1,
          playerCreature2,
          playerCreature3,
        }.AsEnumerable(),
        new[] {opponentCreature});
      return StateTestUtil.EmptyState.New(fields: fields);
    }
  }
}