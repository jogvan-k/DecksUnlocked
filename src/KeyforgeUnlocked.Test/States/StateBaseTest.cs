using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;
using EndTurn = KeyforgeUnlocked.Effects.EndTurn;

namespace KeyforgeUnlockedTest.States
{
  [TestFixture]
  class StateBaseTest
  {
    readonly Dictionary<Player, int> _simpleValues = new Dictionary<Player, int>
    {
      {Player.Player1, 1},
      {Player.Player2, 0}
    };

    readonly Dictionary<Player, ISet<Card>> _simpleSet = new Dictionary<Player, ISet<Card>>
    {
      {Player.Player1, new HashSet<Card>()},
      {Player.Player2, new HashSet<Card>(new[] {new SampleCreatureCard()})}
    };

    readonly Dictionary<Player, Stack<Card>> _simpleStack = new Dictionary<Player, Stack<Card>>
    {
      {Player.Player1, new Stack<Card>()},
      {Player.Player2, new Stack<Card>(new[] {new SampleCreatureCard()})}
    };

    readonly Dictionary<Player, IList<Creature>> _simpleField = new Dictionary<Player, IList<Creature>>
    {
      {Player.Player1, new List<Creature>()},
      {Player.Player2, new List<Creature>(new[] {new Creature(new SampleCreatureCard())})}
    };

    [Test]
    public void Equals_EmptyMutableAndEmptyImmutable()
    {
      MutableState mutableState = StateTestUtil.EmptyMutableState;
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
      Assert.False(emptyState.New(previousstate: emptyState).Equals(emptyState));
      Assert.False(emptyState.New(activeHouse: House.Dis).Equals(emptyState));
      Assert.False(emptyState.New(keys: _simpleValues).Equals(emptyState));
      Assert.False(emptyState.New(aember: _simpleValues).Equals(emptyState));
      Assert.False(emptyState.New(actionGroups: new List<IActionGroup> {new EndTurnGroup()}).Equals(emptyState));
      Assert.False(emptyState.New(decks: _simpleStack).Equals(emptyState));
      Assert.False(emptyState.New(hands: _simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(discards: _simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(archives: _simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(fields: _simpleField).Equals(emptyState));
      Assert.False(emptyState.New(effects: new StackQueue<IEffect>(new[] {new EndTurn()})).Equals(emptyState));
      Assert.False(emptyState.New(resolvedEffects: new List<IResolvedEffect> {new TurnEnded()}).Equals(emptyState));
      Assert.False(
        emptyState.New(
            metadata: new Metadata(
              ImmutableDictionary<Player, Deck>.Empty, ImmutableDictionary<Player, IImmutableSet<House>>.Empty))
          .Equals(emptyState));
    }
  }
}