using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using NUnit.Framework;
using UnlockedCore.States;
using EndTurn = KeyforgeUnlocked.Effects.EndTurn;

namespace KeyforgeUnlockedTest.States
{
  [TestFixture]
  class StateBaseTest
  {
    readonly Dictionary<Player, ISet<Card>> simpleSet = new Dictionary<Player, ISet<Card>>
    {
      {Player.Player1, new HashSet<Card>()},
      {Player.Player2, new HashSet<Card>(new[] {new SimpleCreatureCard()})}
    };

    readonly Dictionary<Player, Stack<Card>> simpleStack = new Dictionary<Player, Stack<Card>>
    {
      {Player.Player1, new Stack<Card>()},
      {Player.Player2, new Stack<Card>(new[] {new SimpleCreatureCard()})}
    };

    readonly Dictionary<Player, IList<Creature>> simpleField = new Dictionary<Player, IList<Creature>>
    {
      {Player.Player1, new List<Creature>()},
      {Player.Player2, new List<Creature>(new[] {new Creature(1, 1, new SimpleCreatureCard())})}
    };

    [Test]
    public void Equals_EmptyMutableAndEmptyImmutable()
    {
      MutableState mutableState = TestUtil.EmptyMutableState;
      ImmutableState immutableState = TestUtil.EmptyState;

      Assert.True(mutableState.Equals(mutableState));
      Assert.True(mutableState.Equals(immutableState));
      Assert.True(immutableState.Equals(mutableState));
      Assert.True(immutableState.Equals(immutableState));
    }

    [Test]
    public void Equals_EmptyAndNonEmpty()
    {
      var emptyState = TestUtil.EmptyMutableState;

      Assert.False(emptyState.New(playerTurn: Player.Player2).Equals(emptyState));
      Assert.False(emptyState.New(turnNumber: 1).Equals(emptyState));
      Assert.False(emptyState.New(isGameOver: true).Equals(emptyState));
      Assert.False(emptyState.New(previousState: emptyState).Equals(emptyState));
      Assert.False(emptyState.New(actionGroups: new List<IActionGroup> {new EndTurnGroup()}).Equals(emptyState));
      Assert.False(emptyState.New(decks: simpleStack).Equals(emptyState));
      Assert.False(emptyState.New(hands: simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(discards: simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(archives: simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(fields: simpleField).Equals(emptyState));
      Assert.False(emptyState.New(effects: new Queue<IEffect>(new[] {new EndTurn()})).Equals(emptyState));
      Assert.False(emptyState.New(resolvedEffects: new List<IResolvedEffect> {new TurnEnded()}).Equals(emptyState));
    }
  }
}