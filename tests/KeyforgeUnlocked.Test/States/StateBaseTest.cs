using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.States
{
  [TestFixture]
  sealed class StateBaseTest
  {
    readonly Lookup<Player, int> _simpleValues = new Dictionary<Player, int>
    {
      {Player.Player1, 1},
      {Player.Player2, 0}
    }.ToLookup();

    readonly IImmutableDictionary<Player, IMutableSet<ICard>> _simpleSet = new Dictionary<Player, IMutableSet<ICard>>
    {
      {Player.Player1, new LazySet<ICard>()},
      {Player.Player2, new LazySet<ICard>(new[] {new SampleCreatureCard()})}
    }.ToImmutableDictionary();

    readonly IImmutableDictionary<Player, IMutableStackQueue<ICard>> _simpleStack = new Dictionary<Player, IMutableStackQueue<ICard>>
    {
      {Player.Player1, new LazyStackQueue<ICard>()},
      {Player.Player2, new LazyStackQueue<ICard>(new[] {new SampleCreatureCard()})}
    }.ToImmutableDictionary();

    readonly IImmutableDictionary<Player, IMutableList<Creature>> _simpleField = new Dictionary<Player, IMutableList<Creature>>
    {
      {Player.Player1, new LazyList<Creature>()},
      {Player.Player2, new LazyList<Creature>(new[] {new Creature(new SampleCreatureCard())})}
    }.ToImmutableDictionary();

    [Test]
    public void Equals_EmptyMutableAndEmptyImmutable()
    {
      IMutableState mutableState = StateTestUtil.EmptyMutableState;
      ImmutableState immutableState = StateTestUtil.EmptyState;

      Assert.True(mutableState.Equals(mutableState));
      Assert.True(mutableState.Equals(immutableState));
      Assert.True(immutableState.Equals(mutableState));
      Assert.True(immutableState.Equals(immutableState));
    }

    [Test]
    public void Equals_EmptyAndNonEmpty()
    {
      var emptyState = StateTestUtil.EmptyMutableState;

      Assert.False(emptyState.New(playerTurn: Player.Player2).Equals(emptyState));
      Assert.False(emptyState.New(turnNumber: 1).Equals(emptyState));
      Assert.False(emptyState.New(isGameOver: true).Equals(emptyState));
      Assert.False(emptyState.New(activeHouse: House.Dis).Equals(emptyState));
      Assert.False(emptyState.New(keys: _simpleValues).Equals(emptyState));
      Assert.False(emptyState.New(aember: _simpleValues).Equals(emptyState));
      Assert.False(emptyState.New(actionGroups: new LazyList<IActionGroup> {new EndTurnGroup()}).Equals(emptyState));
      Assert.False(emptyState.New(decks: _simpleStack).Equals(emptyState));
      Assert.False(emptyState.New(hands: _simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(discards: _simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(archives: _simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(fields: _simpleField).Equals(emptyState));
      Assert.False(emptyState.New(effects: new LazyStackQueue<IEffect>(new[] {new EndTurn()})).Equals(emptyState));
      Assert.False(emptyState.New(resolvedEffects: new LazyList<IResolvedEffect> {new TurnEnded()}).Equals(emptyState));
      Assert.False(
        emptyState.New(
            metadata: new Metadata(
              ImmutableDictionary<Player, Deck>.Empty, ImmutableDictionary<Player, IImmutableSet<House>>.Empty, 0, 0))
          .Equals(emptyState));
    }

    
  }
}