using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.States
{
  [TestFixture]
  sealed class StateExtensions
  {
    [Test]
    public void Steal_FixedAvailableAmber([Range(0, 3)] int stealingAmount)
    {
      var aember = TestUtil.Ints(0, 2);
      var state = StateTestUtil.EmptyState.New(aember: aember);

      state.Steal(stealingAmount);

      var expectedStolen = Math.Min(stealingAmount, 2);
      var expectedAember = TestUtil.Ints(expectedStolen, 2 - expectedStolen);
      var expectedResolvedEffects = new List<IResolvedEffect>();
      if (expectedStolen > 0)
        expectedResolvedEffects.Add(new AemberStolen(Player.Player1, expectedStolen));
      var expectedState = StateTestUtil.EmptyState.New(
        aember: expectedAember, resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects));
      StateAsserter.StateEquals(expectedState, state);
    }

    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void ReturnCreatureToHand(Player player)
    {
      var returnedCreature = new Creature(new SampleCreatureCard());
      var otherCreature = new Creature(new SampleCreatureCard());
      var fields = new Dictionary<Player, IMutableList<Creature>>
      {
        {player, new LazyList<Creature> {returnedCreature}},
        {player.Other(), new LazyList<Creature> {otherCreature}}
      }.ToImmutableDictionary();
      var state = StateTestUtil.EmptyState.New(fields: fields);

      state.ReturnCreatureToHand(returnedCreature);

      var expectedFields = new Dictionary<Player, IMutableList<Creature>>
      {
        {player, new LazyList<Creature>()},
        {player.Other(), new LazyList<Creature> {otherCreature}}
      }.ToImmutableDictionary();
      var expectedHands = new Dictionary<Player, IMutableSet<Card>>
      {
        {player, new LazySet<Card> {returnedCreature.Card}},
        {player.Other(), new LazySet<Card>()}
      }.ToImmutableDictionary();
      var resolvedEffects = new LazyList<IResolvedEffect> {new CreatureReturnedToHand(returnedCreature)};
      var expectedState = StateTestUtil.EmptyState.New(
        fields: expectedFields, hands: expectedHands, resolvedEffects: resolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void LoseAember([Range(0, 3)] int amount)
    {
      var aember = TestUtil.Ints(2, 2);
      var state = StateTestUtil.EmptyState.New(aember: aember);

      state.LoseAember(Player.Player1, amount);

      var expectedLost = Math.Min(amount, 2);
      var expectedResolvedEffects = new LazyList<IResolvedEffect>();
      if (expectedLost > 0)
        expectedResolvedEffects.Add(new AemberLost(Player.Player1, expectedLost));
      var expectedState = StateTestUtil.EmptyState.New(aember: aember, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void CaptureAember([Range(0, 3)] int amount)
    {
      var aember = TestUtil.Ints(2, 2);
      var creature = new Creature(new SampleCreatureCard());
      var opponentCreature = new Creature(new SampleCreatureCard());
      var fields = TestUtil.Lists(creature, opponentCreature);
      var state = StateTestUtil.EmptyState.New(aember: aember, fields: fields);

      state.CaptureAember(creature.Id, amount);

      var expectedCapture = Math.Min(amount, 2);
      var expectedAember = TestUtil.Ints(2, 2 - expectedCapture);
      creature.Aember += expectedCapture;
      var expectedFields = TestUtil.Lists(creature, opponentCreature);
      var expectedResolvedEffects = new LazyList<IResolvedEffect>();
      if (expectedCapture > 0)
        expectedResolvedEffects.Add(new AemberCaptured(creature, expectedCapture));
      var expectedState = StateTestUtil.EmptyState.New(
        aember: expectedAember, fields: expectedFields, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void DestroyCreature_AemberReturned([Range(0, 2)] int aemberOnCreature)
    {
      var creatureCard = new SampleCreatureCard();
      var creature = new Creature(creatureCard, aember: aemberOnCreature);
      var fields = TestUtil.Lists(creature);
      var state = StateTestUtil.EmptyState.New(fields: fields);

      state.DestroyCreature(creature);

      var expectedDiscards = TestUtil.Sets<Card>(creatureCard);
      var expectedAember = TestUtil.Ints(0, aemberOnCreature);
      var expectedResolvedEffects = new LazyList<IResolvedEffect> {new CreatureDied(creature)};
      if (aemberOnCreature > 0)
        expectedResolvedEffects.Add(new AemberClaimed(Player.Player2, aemberOnCreature));
      var expectedState = StateTestUtil.EmptyState.New(
        aember: expectedAember, discards: expectedDiscards, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}