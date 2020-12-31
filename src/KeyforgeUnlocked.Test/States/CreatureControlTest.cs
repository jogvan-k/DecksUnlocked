using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.States
{
  [TestFixture]
  sealed class StateExtensions
  {
    [Test]
    public void DamageCreature(
      [Values(Player.Player1, Player.Player2)]Player player,
      [Range(0, 3)] int damage)
    {
      var targetCreatureCard = new SampleCreatureCard(power: 2);
      var targetCreature = new Creature(targetCreatureCard);
      var otherCreature = new Creature(new SampleCreatureCard());
      var fields = InstantiateFields(player, targetCreature, otherCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);
      
      state.DamageCreature(targetCreature, damage);

      LookupReadOnly<Player, IMutableList<Creature>> expectedFields;
      var expectedDiscards = TestUtil.Sets<ICard>();
      var expectedTarget = new Creature(targetCreatureCard, damage: damage);
      var expectedResolvedEffects = new List<IResolvedEffect>();
      
      if (damage < 2)
      {
        expectedFields = InstantiateFields(player, expectedTarget, otherCreature);
      }
      else
      {
        expectedFields = new Dictionary<Player, IMutableList<Creature>>
        {
          {player, new LazyList<Creature>()},
          {player.Other(), new LazyList<Creature>{otherCreature}}
        }.ToReadOnly();
      }

      if(damage > 0)
        expectedResolvedEffects.Add(new CreatureDamaged(expectedTarget, damage));

      if (damage > 1)
      {
        expectedResolvedEffects.Add(new CreatureDied(expectedTarget));
        expectedDiscards[player].Add(targetCreatureCard);
      }

      var expectedState = StateTestUtil.EmptyState.New(
        fields: expectedFields,
        resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects),
        discards: expectedDiscards);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void HealCreature(
      [Values(Player.Player1, Player.Player2)]Player player,
      [Range(0, 3)] int amount)
    {
      var targetCreatureCard = new SampleCreatureCard(power: 3);
      var targetCreature = new Creature(targetCreatureCard, damage: 2);
      var otherCreature = new Creature(new SampleCreatureCard(), damage: 2);
      var fields = InstantiateFields(player, targetCreature, otherCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);

      var healedAmount = state.HealCreature(targetCreature, amount);

      var expectedHealedAmount = Math.Min(2, amount);

      Assert.That(healedAmount, Is.EqualTo(expectedHealedAmount));
      var expectedTarget = new Creature(targetCreatureCard, damage: Math.Max(0, 2 - amount));
      var expectedResolvedEffects = new List<IResolvedEffect>();

      var expectedFields = InstantiateFields(player, expectedTarget, otherCreature);

      if (amount > 0)
        expectedResolvedEffects.Add(new CreatureHealed(expectedTarget, expectedHealedAmount));

      var expectedState = StateTestUtil.EmptyState.New(
        fields: expectedFields,
        resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects));
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void HealCreature_CreatureAtFullHealth_NoEffect(
      [Values(Player.Player1, Player.Player2)] Player player)
    {
      var targetCreatureCard = new SampleCreatureCard(power: 3);
      var targetCreature = new Creature(targetCreatureCard);
      var otherCreature = new Creature(new SampleCreatureCard(), damage: 2);
      var fields = InstantiateFields(player, targetCreature, otherCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);

      var healedAmount = state.HealCreature(targetCreature, 2);

      Assert.That(healedAmount, Is.EqualTo(0));
      var expectedFields = InstantiateFields(player, targetCreature, otherCreature);
      var expectedState = StateTestUtil.EmptyState.New(fields: expectedFields);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void ReturnCreatureToHand([Values(Player.Player1, Player.Player2)]Player player)
    {
      var returnedCreature = new Creature(new SampleCreatureCard());
      var otherCreature = new Creature(new SampleCreatureCard());
      var fields = new Dictionary<Player, IMutableList<Creature>>
      {
        {player, new LazyList<Creature> {returnedCreature}},
        {player.Other(), new LazyList<Creature> {otherCreature}}
      }.ToImmutableDictionary();
      var events = new LazyEvents();
      var creatureReturnedToHandEventInvoked = false;
      events.Subscribe(new Identifiable(""), EventType.CreatureReturnedToHand, (_, _, _) => creatureReturnedToHandEventInvoked = true);
      var state = StateTestUtil.EmptyState.New(fields: fields, events: events);

      state.ReturnCreatureToHand(returnedCreature);

      var expectedFields = new Dictionary<Player, IMutableList<Creature>>
      {
        {player, new LazyList<Creature>()},
        {player.Other(), new LazyList<Creature> {otherCreature}}
      }.ToImmutableDictionary();
      var expectedHands = new Dictionary<Player, IMutableSet<ICard>>
      {
        {player, new LazySet<ICard> {returnedCreature.Card}},
        {player.Other(), new LazySet<ICard>()}
      }.ToImmutableDictionary();
      var resolvedEffects = new LazyList<IResolvedEffect> {new CreatureReturnedToHand(returnedCreature)};
      var expectedState = StateTestUtil.EmptyState.New(
        fields: expectedFields, hands: expectedHands, events: events, resolvedEffects: resolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
      Assert.True(creatureReturnedToHandEventInvoked);
    }

    [Test]
    public void DestroyCreature_AemberReturned([Range(0, 2)] int aemberOnCreature)
    {
      var creatureCard = new SampleCreatureCard();
      var creature = new Creature(creatureCard, aember: aemberOnCreature);
      var fields = TestUtil.Lists(creature);
      var state = StateTestUtil.EmptyState.New(fields: fields);

      state.DestroyCreature(creature);

      var expectedDiscards = TestUtil.Sets<ICard>(creatureCard);
      var expectedAember = TestUtil.Ints(0, aemberOnCreature);
      var expectedResolvedEffects = new LazyList<IResolvedEffect> {new CreatureDied(creature)};
      if (aemberOnCreature > 0)
        expectedResolvedEffects.Add(new AemberClaimed(Player.Player2, aemberOnCreature));
      var expectedState = StateTestUtil.EmptyState.New(
        aember: expectedAember, discards: expectedDiscards, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void AddAemberToCreature(
      [Values(Player.Player1, Player.Player2)] Player player,
      [Range(0, 2)] int amount)
    {
      var targetCreatureCard = new SampleCreatureCard();
      var targetCreature = new Creature(targetCreatureCard);
      var otherCreature = new Creature(new SampleCreatureCard());
      var fields = InstantiateFields(player, targetCreature, otherCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);

      state.AddAemberToCreature(targetCreature, amount);

      var expectedTargetCreature = new Creature(targetCreatureCard, aember: amount);
      var expectedFields = InstantiateFields(player, expectedTargetCreature, otherCreature);
      var resolvedEffects = new List<IResolvedEffect>();
      if(amount > 0)
        resolvedEffects.Add(new CreatureGainedAember(expectedTargetCreature, amount));
      var expectedState = StateTestUtil.EmptyState.New(fields: expectedFields, resolvedEffects: new LazyList<IResolvedEffect>(resolvedEffects));
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void StunCreature(
      [Values(true, false)] bool targetAlreadyStunned)
    {
      var targetCreatureCard = new SampleCreatureCard();
      var targetCreature = new Creature(targetCreatureCard, state: targetAlreadyStunned ? CreatureState.Stunned : CreatureState.None);
      var otherCreature = new Creature(new SampleCreatureCard());
      var fields = InstantiateFields(Player.Player1, targetCreature, otherCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);

      state.StunCreature(targetCreature);

      var expectedFields =
        InstantiateFields(Player.Player1, new Creature(targetCreatureCard, state: CreatureState.Stunned), otherCreature);

      var expectedState = StateTestUtil.EmptyState.New(fields: expectedFields);
      if(!targetAlreadyStunned)
        expectedState.ResolvedEffects.Add(new CreatureStunned(targetCreatureCard));
      
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void ExhaustCreature(
      [Values(true, false)] bool targetAlreadyExhausted)
    {
      var targetCreatureCard = new SampleCreatureCard();
      var targetCreature = new Creature(targetCreatureCard, isReady: !targetAlreadyExhausted);
      var otherCreature = new Creature(new SampleCreatureCard());
      var fields = InstantiateFields(Player.Player1, targetCreature, otherCreature);
      var state = StateTestUtil.EmptyState.New(fields: fields);

      state.ExhaustCreature(targetCreature);

      var expectedFields =
        InstantiateFields(Player.Player1, new Creature(targetCreatureCard), otherCreature);

      var expectedState = StateTestUtil.EmptyState.New(fields: expectedFields);
      if(!targetAlreadyExhausted)
        expectedState.ResolvedEffects.Add(new CreatureExhausted(targetCreatureCard));
      
      StateAsserter.StateEquals(expectedState, state);
    }
    
    static LookupReadOnly<Player, IMutableList<Creature>> InstantiateFields(Player player, Creature targetCreature, Creature otherCreature)
    {
      return TestUtil.Lists(
        player.IsPlayer1() ? targetCreature : otherCreature,
        player.IsPlayer2() ? targetCreature : otherCreature);
    }
  }
}