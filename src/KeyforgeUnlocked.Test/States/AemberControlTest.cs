using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.States
{
  [TestFixture]
  sealed class AemberControlTest
  {
    [Test, Combinatorial]
    public void GainAember(
      [Values(Player.Player1, Player.Player2)] Player player,
      [Range(0, 2)] int amount)
    {
      var aember = TestUtil.Ints(2, 2);
      var state = StateTestUtil.EmptyState.New(aember: aember);
      
      state.GainAember(player, amount);

      var expectedAember =
        player.IsPlayer1()
          ? TestUtil.Ints(2 + amount, 2)
          : TestUtil.Ints(2, 2 + amount);
      var expectedResolvedEffects = new List<IResolvedEffect>();
      if(amount > 0) expectedResolvedEffects.Add(new AemberGained(player, amount));
      var expectedState = StateTestUtil.EmptyState.New(
        aember: expectedAember, resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects));
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test, Combinatorial]
    public void StealAember_FixedAvailableAmber(
      [Values(Player.Player1, Player.Player2)] Player stealingPlayer,
      [Range(0, 3)] int stealingAmount)
    {
      var aember = TestUtil.Ints(2, 2);
      var state = StateTestUtil.EmptyState.New(aember: aember);

      state.StealAember(stealingPlayer, stealingAmount);

      var expectedStolen = Math.Min(stealingAmount, 2);
      var expectedAember =
        stealingPlayer.IsPlayer1()
          ? TestUtil.Ints(2 + expectedStolen, 2 - expectedStolen)
          : TestUtil.Ints(2 - expectedStolen, 2 + expectedStolen);
      var expectedResolvedEffects = new List<IResolvedEffect>();
      if (expectedStolen > 0)
        expectedResolvedEffects.Add(new AemberStolen(stealingPlayer, expectedStolen));
      var expectedState = StateTestUtil.EmptyState.New(
        aember: expectedAember, resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects));
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void LoseAember(
      [Values(Player.Player1, Player.Player2)] Player player,
      [Range(0, 3)] int amount)
    {
      var aember = TestUtil.Ints(2, 2);
      var state = StateTestUtil.EmptyState.New(aember: aember);

      state.LoseAember(player, amount);

      var expectedLost = Math.Min(amount, 2);
      var expectedResolvedEffects = new LazyList<IResolvedEffect>();
      if (expectedLost > 0)
        expectedResolvedEffects.Add(new AemberLost(player, expectedLost));
      var expectedAember = player.IsPlayer1()
        ? TestUtil.Ints(2 - expectedLost, 2)
        : TestUtil.Ints(2, 2 - expectedLost);
      var expectedState = StateTestUtil.EmptyState.New(aember: expectedAember, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void CaptureAember(
      [Values(Player.Player1, Player.Player2)] Player player,
      [Range(0, 3)] int amount)
    {
      var aember = TestUtil.Ints(2, 2);
      var creature = new Creature(new SampleCreatureCard());
      var opponentCreature = new Creature(new SampleCreatureCard());
      var fields = 
        player.IsPlayer1() 
          ? TestUtil.Lists(creature, opponentCreature)
          : TestUtil.Lists(opponentCreature, creature);
      var state = StateTestUtil.EmptyState.New(aember: aember, fields: fields);

      state.CaptureAember(creature, amount);

      var expectedCapture = Math.Min(amount, 2);
      var expectedAember = 
        player.IsPlayer1()
      ? TestUtil.Ints(2, 2 - expectedCapture)
      : TestUtil.Ints(2 - expectedCapture, 2);
      creature.Aember += expectedCapture;
      var expectedFields = 
        player.IsPlayer1() 
          ? TestUtil.Lists(creature, opponentCreature)
          : TestUtil.Lists(opponentCreature, creature);
      var expectedResolvedEffects = new LazyList<IResolvedEffect>();
      if (expectedCapture > 0)
        expectedResolvedEffects.Add(new AemberCaptured(creature, expectedCapture));
      var expectedState = StateTestUtil.EmptyState.New(
        aember: expectedAember, fields: expectedFields, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }
    
  }
}