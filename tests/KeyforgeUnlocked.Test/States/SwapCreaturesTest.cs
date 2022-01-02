using System.Linq;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

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
        state.SwapCreatures(creature, _playerCreature2);
      }
      catch (CreatureNotPresentException e)
      {
        Assert.True(e.Id.Equals(creature));
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
        state.SwapCreatures(_playerCreature1, _opponentCreature);
      }
      catch (CreatureNotPresentException e)
      {
        Assert.True(e.Id.Equals(_opponentCreature));
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void SwapCreatures()
    {
      var state = SetupState();

      state.SwapCreatures(_playerCreature1, _playerCreature3);

      var expectedState = SetupState(_playerCreature3, _playerCreature2, _playerCreature1, _opponentCreature);
      expectedState.ResolvedEffects.Add(new CreaturesSwapped(_playerCreature1, _playerCreature3));
      StateAsserter.StateEquals(expectedState, state);
    }

    IMutableState SetupState()
    {
      return SetupState(_playerCreature1, _playerCreature2, _playerCreature3, _opponentCreature);
    }

    IMutableState SetupState(Creature playerCreature1, Creature playerCreature2, Creature playerCreature3,
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