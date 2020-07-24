using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  sealed class TargetCreatureTest
  {
    Creature playerOneCreature = new Creature(new SampleCreatureCard());
    Creature playerTwoCreature = new Creature(new SampleCreatureCard());

    [Test]
    public void Resolve_TwoValidTarets_CreateActions()
    {
      var state = Setup();
      EffectOnCreature effectOnCreature = (s, c) => { };
      var sut = new TargetCreature(effectOnCreature, Delegates.All);

      sut.Resolve(state);

      var expectedActionGroup = new TargetCreatureGroup(
        new[]
        {
          new KeyforgeUnlocked.Actions.TargetCreature(effectOnCreature, playerTwoCreature),
          new KeyforgeUnlocked.Actions.TargetCreature(effectOnCreature, playerOneCreature)
        });

      var expectedState = Setup().New(actionGroups: new IActionGroup[]{expectedActionGroup});

      StateAsserter.StateEquals(expectedState, state);
    }

    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void Resolve_OneValidTaret_DoEffectOnTarget(Player targetPlayerCreature)
    {
      var state = Setup();
      Creature target = default;
      EffectOnCreature effect = (s, c) => target = c;
      ValidOn validOn = (s, c) => state.ControllingPlayer(c).Equals(targetPlayerCreature);
      var sut = new TargetCreature(effect, validOn);

      sut.Resolve(state);

      StateAsserter.StateEquals(Setup(), state);
      var expectedTarget = targetPlayerCreature == Player.Player1 ? playerOneCreature : playerTwoCreature;
      Assert.AreEqual(expectedTarget, target);
    }

    [Test]
    public void Resolve_NoValidTarets_NoEffect()
    {
      var state = Setup();
      bool effectResolved = false;
      EffectOnCreature effect = (s, c) => effectResolved = true;
      ValidOn validOn = (s, c) => false;
      var sut = new TargetCreature(effect, validOn);

      sut.Resolve(state);

      StateAsserter.StateEquals(Setup(), state);
      Assert.False(effectResolved);
    }

    MutableState Setup()
    {
      var fields = TestUtil.Lists(playerOneCreature, playerTwoCreature);
      return StateTestUtil.EmptyState.New(fields: fields);
    }
  }
}