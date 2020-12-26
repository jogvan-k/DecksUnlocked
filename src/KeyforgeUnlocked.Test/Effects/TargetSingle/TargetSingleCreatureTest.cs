using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects.TargetSingle
{
  [TestFixture]
  sealed class TargetSingleCreatureTest
  {
    Creature playerOneCreature = new(new SampleCreatureCard());
    Creature playerTwoCreature = new(new SampleCreatureCard());

    [Test]
    public void Resolve_TwoValidTargets_CreateActions()
    {
      var state = Setup();
      var effectResolved = false;
      EffectOnTarget effectOnTarget = (_, _) => effectResolved = true;
      var sut = new TargetSingleCreature(effectOnTarget);

      sut.Resolve(state);

      var expectedActionGroup = new SingleTargetGroup(effectOnTarget,
        new[] {(IIdentifiable) playerTwoCreature, playerOneCreature}.ToImmutableList());

      var expectedState = Setup().New(actionGroups: new LazyList<IActionGroup> {expectedActionGroup});

      Assert.False(effectResolved);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void Resolve_OneValidTarget_DoEffectOnTarget(
      [Values(Player.Player1, Player.Player2)]
      Player targetPlayerCreature)
    {
      var state = Setup();
      IIdentifiable target = default;
      EffectOnTarget effect = (_, c) => target = c;
      ValidOn validOn = (_, c) => state.ControllingPlayer(c).Equals(targetPlayerCreature);
      var sut = new TargetSingleCreature(effect, validOn: validOn);

      sut.Resolve(state);

      StateAsserter.StateEquals(Setup(), state);
      var expectedTarget = targetPlayerCreature == Player.Player1 ? playerOneCreature : playerTwoCreature;
      Assert.AreEqual(expectedTarget, target);
    }

    [Test]
    public void Resolve_NoValidTargets_NoEffect()
    {
      var state = Setup();
      bool effectResolved = false;
      EffectOnTarget effect = (_, _) => effectResolved = true;
      ValidOn validOn = (_, _) => false;
      var sut = new TargetSingleCreature(effect, validOn: validOn);

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