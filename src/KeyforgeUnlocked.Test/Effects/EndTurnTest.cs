using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  public class EndTurnTest
  {
    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void Resolve_NextPlayerAndIncreaseTurnNumber(Player playerTurn)
    {
      var turnNumberStart = 1;
      var endTurnEventInvoked = false;
      var events = new LazyEvents();
      events.Subscribe(new Identifiable(""), EventType.TurnEnded, (_, _, _) => endTurnEventInvoked = true);
      var state = StateTestUtil.EmptyMutableState.New(playerTurn, turnNumberStart, activeHouse: House.Brobnar, events: events);
      var sut = new EndTurn();

      sut.Resolve(state);

      var expectedEffects = new LazyStackQueue<IEffect>(new[] {(IEffect) new DeclareHouse(), new TryForge(), new CheckGameTurnLimit()});
      var expectedState = StateTestUtil.EmptyMutableState.New(
        playerTurn.Other(),
        turnNumberStart + 1,
        resolvedEffects: new LazyList<IResolvedEffect> {new TurnEnded()},
        effects: expectedEffects,
        events: events);
      Assert.True(endTurnEventInvoked);
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}