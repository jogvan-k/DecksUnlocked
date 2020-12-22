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