using System.Collections.Generic;
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
    readonly Dictionary<Player, int> simpleValues = new Dictionary<Player, int>
    {
      {Player.Player1, 1},
      {Player.Player2, 0}
    };
    
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
      Assert.False(emptyState.New(previousState: emptyState).Equals(emptyState));
      Assert.False(emptyState.New(keys: simpleValues).Equals(emptyState));
      Assert.False(emptyState.New(aember: simpleValues).Equals(emptyState));
      Assert.False(emptyState.New(actionGroups: new List<IActionGroup> {new EndTurnGroup()}).Equals(emptyState));
      Assert.False(emptyState.New(decks: simpleStack).Equals(emptyState));
      Assert.False(emptyState.New(hands: simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(discards: simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(archives: simpleSet).Equals(emptyState));
      Assert.False(emptyState.New(fields: simpleField).Equals(emptyState));
      Assert.False(emptyState.New(effects: new StackQueue<IEffect>(new[] {new EndTurn()})).Equals(emptyState));
      Assert.False(emptyState.New(resolvedEffects: new List<IResolvedEffect> {new TurnEnded()}).Equals(emptyState));
    }
  }
}