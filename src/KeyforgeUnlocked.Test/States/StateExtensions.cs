using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

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
        aember: expectedAember, resolvedEffects: expectedResolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }

    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void ReturnCreatureToHand(Player player)
    {
      var returnedCreature = new Creature(new SampleCreatureCard());
      var otherCreature = new Creature(new SampleCreatureCard());
      var fields = new Dictionary<Player, IList<Creature>>
      {
        {player, new List<Creature> {returnedCreature}},
        {player.Other(), new List<Creature> {otherCreature}}
      };
      var state = StateTestUtil.EmptyState.New(fields: fields);

      state.ReturnCreatureToHand(returnedCreature);

      var expectedFields = new Dictionary<Player, IList<Creature>>
      {
        {player, new List<Creature>()},
        {player.Other(), new List<Creature> {otherCreature}}
      };
      var expectedHands = new Dictionary<Player, ISet<Card>>
      {
        {player, new HashSet<Card> {returnedCreature.Card}},
        {player.Other(), new HashSet<Card>()}
      };
      var resolvedEffects = new List<IResolvedEffect> { new CreatureReturnedToHand(returnedCreature)};
      var expectedState = StateTestUtil.EmptyState.New(fields: expectedFields, hands: expectedHands, resolvedEffects: resolvedEffects);
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}