using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Artifacts;
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
  sealed class PlayArtifactCardTest
  {
    [Test]
    public void Resolve_CardWithPlayAbility()
    {
      var playAbilityResolved = false;
      var playCardEventRaised = false;
      Callback playAbility = (_, _, _) => playAbilityResolved = true;
      Callback playCardEvent = (_, _, _) => playCardEventRaised = true;
      var card = MockArtifactCard(playAbility);
      var events = new LazyEvents();
      events.Subscribe(card, EventType.CardPlayed, playCardEvent);

      var sut = new PlayArtifactCard(card);
      var state = StateTestUtil.EmptyMutableState.New(events: events);

      sut.Resolve(state);

      var expectedArtifacts = TestUtil.Sets<Artifact>(new Artifact(card));
      var expectedResolvedEffects = new List<IResolvedEffect> {new ArtifactCardPlayed(card)};
      var expectedState = StateTestUtil.EmptyState.New(artifacts: expectedArtifacts,
        resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects), events: events);
      StateAsserter.StateEquals(expectedState, state);
      Assert.True(playAbilityResolved);
      Assert.True(playCardEventRaised);
    }

    [Test]
    public void Resolve_CardWithAemberPips(
      [Values(Player.Player1, Player.Player2)] Player playerTurn)
    {
      var playCardEventRaised = false;
      Callback playCardEvent = (_, _, _) => playCardEventRaised = true;
      var card = MockArtifactCard(null, new[] {Pip.Aember, Pip.Aember, Pip.Aember});
      var events = new LazyEvents();
      events.Subscribe(card, EventType.CardPlayed, playCardEvent);
      var sut = new PlayArtifactCard(card);
      var state = StateTestUtil.EmptyMutableState.New(playerTurn, events: events);

      sut.Resolve(state);

      var expectedAember = TestUtil.Ints();
      expectedAember[playerTurn] = 3;
      var expectedResolvedEffects = new List<IResolvedEffect>
      {
        new ArtifactCardPlayed(card),
        new AemberGained(playerTurn, 1),
        new AemberGained(playerTurn, 1),
        new AemberGained(playerTurn, 1)
      };

      var expectedArtifacts = TestUtil.Sets<Artifact>();
      expectedArtifacts[playerTurn].Add(new Artifact(card));
      var expectedState = StateTestUtil.EmptyState.New(playerTurn, aember: expectedAember, artifacts: expectedArtifacts, 
        resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects), events: events);
      StateAsserter.StateEquals(expectedState, state);
      Assert.True(playCardEventRaised);
    }

    [Test]
    public void Resolve_CardsWithUnimplementedPips_NotImplementedException([Values(Pip.Capture, Pip.Damage, Pip.Draw)]
      Pip pip)
    {
      var card = MockArtifactCard(pips: new[] {pip});
      var sut = new PlayArtifactCard(card);
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

    static IArtifactCard MockArtifactCard(Callback playAbility = null, Pip[] pips = null)
    {
      var cardMock = new Mock<IArtifactCard>(MockBehavior.Strict);
      cardMock.Setup(c => c.Id).Returns("Id");
      cardMock.Setup(c => c.CardPlayAbility).Returns(playAbility);
      cardMock.Setup(c => c.CardPips).Returns(pips ?? Array.Empty<Pip>());

      return cardMock.Object;
    }
  }
}