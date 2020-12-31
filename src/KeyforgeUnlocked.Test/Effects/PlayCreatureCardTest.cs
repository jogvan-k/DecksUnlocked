using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlockedTest.Util;
using Moq;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class PlayCreatureCardTest
  {
    Callback _playAbility;
    Callback _playCardEvent;
    bool _playedEffectResolved;
    bool _playCardEventRaised;

    static readonly ICard[] otherCards =
      {new SampleCreatureCard(), new SampleCreatureCard(), new SampleCreatureCard()};

    static readonly ICreatureCard CreatureCardOnBoard1 = new SampleCreatureCard();
    static readonly ICreatureCard CreatureCardOnBoard2 = new SampleCreatureCard();

    [SetUp]
    public void SetUp()
    {
      _playedEffectResolved = false;
      _playCardEventRaised = false;
      _playAbility = (_, _, _) => { _playedEffectResolved = true; };
      _playCardEvent = (_, _, _) => _playCardEventRaised = true;
    }

    [Test]
    public void Resolve_EmptyBoard(
      [Values(Player.Player1, Player.Player2)] Player playingPlayer)
    {
      var state = TestState(playingPlayer);
      var playedCard = MockPlayedCreatureCard(_playAbility);
      var sut = new PlayCreatureCard(playedCard, 0);

      sut.Resolve(state);

      var expectedState = TestState(playingPlayer);
      var expectedCreature = new Creature(playedCard);
      expectedState.Fields[playingPlayer].Add(expectedCreature);
      expectedState.ResolvedEffects.Add(new CreatureCardPlayed(expectedCreature, 0));
      StateAsserter.StateEquals(expectedState, state);
      Assert.True(_playedEffectResolved);
      Assert.True(_playCardEventRaised);
    }

    [Test]
    public void Resolve_EmptyBoard_InvalidPosition([Values(-1, 1) ]int position)
    {
      var state = StateTestUtil.EmptyMutableState;
      var playedCard = MockPlayedCreatureCard(_playAbility);

      var sut = new PlayCreatureCard(
        playedCard,
        position);
      try
      {
        sut.Resolve(state);
      }
      catch (InvalidBoardPositionException e)
      {
        Assert.AreEqual(position, e.boardPosition);
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void Resolve_TwoCreaturesOnBoard([Range(0, 2)]int position)
    {
      var playingPlayer = Player.Player2;
      var state = StateWithTwoCreatures(playingPlayer);
      var playedCard = MockPlayedCreatureCard(_playAbility);

      var sut = new PlayCreatureCard(playedCard, position);

      sut.Resolve(state);

      var expectedState = StateWithTwoCreatures(playingPlayer);
      var expectedCreature = new Creature(playedCard);
      expectedState.Fields[playingPlayer].Insert(position, expectedCreature);
      expectedState.ResolvedEffects.Add(new CreatureCardPlayed(expectedCreature, position));
      expectedState.Events = Events();
      StateAsserter.StateEquals(expectedState, state);
      Assert.True(_playedEffectResolved);
      Assert.True(_playCardEventRaised);
    }

    [Test]
    public void Resolve_TwoCreaturesOnBoard_InvalidPosition([Values(-1, 3)]int position)
    {
      var playedCard = MockPlayedCreatureCard(_playAbility);

      var state = StateWithTwoCreatures(Player.Player2);
      var sut = new PlayCreatureCard(playedCard, position);

      try
      {
        sut.Resolve(state);
      }
      catch (InvalidBoardPositionException e)
      {
        Assert.AreEqual(position, e.boardPosition);
        return;
      }

      Assert.Fail();
    }

    [Test]
    public void Resolve_CardWithPips([Values(Player.Player1, Player.Player2)] Player activePlayer)
    {
      var playedCard = MockPlayedCreatureCard(pips: new [] {Pip.Aember, Pip.Aember, Pip.Aember});

      var state = TestState(activePlayer);
      var sut = new PlayCreatureCard(playedCard, 0);
      
      sut.Resolve(state);

      var expectedState = TestState(activePlayer);
      expectedState.Aember[activePlayer] += 3;
      expectedState.ResolvedEffects.Add(new CreatureCardPlayed(new Creature(playedCard), 0));
      expectedState.ResolvedEffects.Add(new AemberGained(activePlayer, 1));
      expectedState.ResolvedEffects.Add(new AemberGained(activePlayer, 1));
      expectedState.ResolvedEffects.Add(new AemberGained(activePlayer, 1));
      expectedState.Fields[activePlayer].Add(new Creature(playedCard));
      
      StateAsserter.StateEquals(expectedState, state);
      Assert.False(_playedEffectResolved);
      Assert.True(_playCardEventRaised);
    }

    IMutableState TestState(Player playingPlayer)
    {
      var hands = new Dictionary<Player, IMutableSet<ICard>>
      {
        {playingPlayer, new LazySet<ICard> {otherCards[0]}},
        {playingPlayer.Other(), new LazySet<ICard> {otherCards[1], otherCards[2]}}
      }.ToImmutableDictionary();

      return StateTestUtil.EmptyMutableState.New(playingPlayer, hands: hands, events: Events());
    }

    IMutableEvents Events()
    {
      var events = new LazyEvents();
      events.Subscribe(new Identifiable(""), EventType.CardPlayed, _playCardEvent);
      return events;
    } 

    IMutableState StateWithTwoCreatures(Player playingPlayer)
    {
      var creature1 = new Creature(CreatureCardOnBoard1);
      var creature2 = new Creature(CreatureCardOnBoard2);

      var state = TestState(playingPlayer);
      state.Fields[Player.Player2].Clear();
      state.Fields[Player.Player2].Add(creature1);
      state.Fields[Player.Player2].Add(creature2);
      return state;
    }

    ICreatureCard MockPlayedCreatureCard(Callback playAbility = null, Pip[] pips = null)
    {
      var mock = new Mock<ICreatureCard>(MockBehavior.Strict);
      mock.Setup(c => c.InsantiateCreature()).Returns(new Creature(mock.Object));
      mock.Setup(c => c.CardPlayAbility).Returns(playAbility);
      mock.Setup(c => c.Id).Returns("Id");
      mock.Setup(c => c.CardPips).Returns(pips ?? Array.Empty<Pip>());
      return mock.Object;
    }
  }
}