using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class PlayCreatureTest
  {
    bool _playedEffectResolved;
    CreatureCard PlayedCard;

    static readonly Card[] otherCards =
      {new SampleCreatureCard(), new SampleCreatureCard(), new SampleCreatureCard()};

    static readonly CreatureCard CreatureCardOnBoard1 = new SampleCreatureCard();
    static readonly CreatureCard CreatureCardOnBoard2 = new SampleCreatureCard();

    [SetUp]
    public void SetUp()
    {
      _playedEffectResolved = false;
      Callback playAbility = (s, id) => { _playedEffectResolved = true; };
      PlayedCard = new SampleCreatureCard(playAbility: playAbility);
    }

    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void Resolve_EmptyBoard(Player playingPlayer)
    {
      var state = TestState(playingPlayer);
      var sut = new PlayCreatureCard(PlayedCard, 0);

      sut.Resolve(state);

      var expectedState = TestState(playingPlayer);
      var expectedCreature = new Creature(PlayedCard);
      expectedState.Fields[playingPlayer].Add(expectedCreature);
      expectedState.ResolvedEffects.Add(new CreaturePlayed(expectedCreature, 0));
      StateAsserter.StateEquals(expectedState, state);
      Assert.True(_playedEffectResolved);
    }

    [TestCase(-1)]
    [TestCase(1)]
    public void Resolve_EmptyBoard_InvalidPosition(int position)
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new PlayCreatureCard(
        PlayedCard,
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

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(2)]
    public void Resolve_TwoCreaturesOnBoard(int position)
    {
      var playingPlayer = Player.Player2;
      var state = StateWithTwoCreatures(playingPlayer);
      var sut = new PlayCreatureCard(PlayedCard, position);

      sut.Resolve(state);

      var expectedState = StateWithTwoCreatures(playingPlayer);
      var expectedCreature = new Creature(PlayedCard);
      expectedState.Fields[playingPlayer].Insert(position, expectedCreature);
      expectedState.ResolvedEffects.Add(new CreaturePlayed(expectedCreature, position));
      StateAsserter.StateEquals(expectedState, state);
      Assert.True(_playedEffectResolved);
    }

    [TestCase(-1)]
    [TestCase(3)]
    public void Resolve_TwoCreaturesOnBoard_InvalidPosition(int position)
    {
      var state = StateWithTwoCreatures(Player.Player2);
      var sut = new PlayCreatureCard(PlayedCard, position);

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

    MutableState TestState(Player playingPlayer)
    {
      var hands = new Dictionary<Player, ISet<Card>>
      {
        {playingPlayer, new HashSet<Card> {otherCards[0]}},
        {playingPlayer.Other(), new HashSet<Card> {otherCards[1], otherCards[2]}}
      };
      return StateTestUtil.EmptyMutableState.New(playingPlayer, hands: hands);
    }

    MutableState StateWithTwoCreatures(Player playingPlayer)
    {
      var creature1 = new Creature(CreatureCardOnBoard1);
      var creature2 = new Creature(CreatureCardOnBoard2);

      var state = TestState(playingPlayer);
      state.Fields[Player.Player2] = new List<Creature> {creature1, creature2};
      return state;
    }
  }
}