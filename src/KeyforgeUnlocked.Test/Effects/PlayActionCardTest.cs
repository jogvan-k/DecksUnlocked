using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlockedTest.Util;
using Moq;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  sealed class PlayActionCardTest
  {
    [Test]
    public void Resolve_CardWithPlayAbility()
    {
      var playAbilityResolved = false;
      var playCardEventRaised = false;
      Callback playAbility = (_, _, _) => playAbilityResolved = true;
      Callback playCardEvent = (_, _, _) => playCardEventRaised = true;
      var card = MockActionCard(playAbility);
      var events = new LazyEvents();
      events.Subscribe(card, EventType.CardPlayed, playCardEvent);

      var sut = new PlayActionCard(card);
      var state = StateTestUtil.EmptyMutableState.New(events: events);

      sut.Resolve(state);

      var expectedDiscards = TestUtil.Sets<ICard>(card);
      var expectedResolvedEffects = new List<IResolvedEffect>{new ActionCardPlayed(card)};
      var expectedState = StateTestUtil.EmptyState.New(discards: expectedDiscards, resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects), events: events);
      StateAsserter.StateEquals(expectedState, state);
      Assert.True(playAbilityResolved);
      Assert.True(playCardEventRaised);
    }

    [Test]
    public void Resolve_CardWithAemberPips()
    {
      var card = MockActionCard(null, new [] {Pip.Aember, Pip.Aember, Pip.Aember});
      var playCardEventRaised = false;
      Callback playCardEvent = (_, _, _) => playCardEventRaised = true;
      var sut = new PlayActionCard(card);
      var events = new LazyEvents();
      events.Subscribe(card, EventType.CardPlayed, playCardEvent);
      var state = StateTestUtil.EmptyMutableState.New(events: events);

      sut.Resolve(state);
      
      var expectedDiscards = TestUtil.Sets<ICard>(card);
      var expectedAember = TestUtil.Ints(3, 0);
      var expectedResolvedEffects = new List<IResolvedEffect>
      {
        new ActionCardPlayed(card),
        new AemberGained(Player.Player1, 1),
        new AemberGained(Player.Player1, 1),
        new AemberGained(Player.Player1, 1)
      };
      
      var expectedState = StateTestUtil.EmptyState.New(aember:expectedAember, discards: expectedDiscards, resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects), events: events);
      StateAsserter.StateEquals(expectedState, state);
      Assert.True(playCardEventRaised);
    }

    [Test]
    public void Resolve_CardsWithUnimplementedPips_NotImplementedException([Values(Pip.Capture, Pip.Damage, Pip.Draw)]Pip pip)
    {
      var card = MockActionCard(pips: new[] {pip});
      var sut = new PlayActionCard(card);
      var state = StateTestUtil.EmptyMutableState;

      try
      {
        sut.Resolve(state);
      }
      catch (NotImplementedException)
      {
        return;
      }

      Assert.Fail();
    }

    static IActionCard MockActionCard(Callback playAbility = null, Pip[] pips = null)
    {
      var cardMock = new Mock<IActionCard>(MockBehavior.Strict);
      cardMock.Setup(c => c.Id).Returns("Id");
      cardMock.Setup(c => c.CardPlayAbility).Returns(playAbility);
      cardMock.Setup(c => c.CardPips).Returns(pips ?? Array.Empty<Pip>());

      return cardMock.Object;
    }
  }
}