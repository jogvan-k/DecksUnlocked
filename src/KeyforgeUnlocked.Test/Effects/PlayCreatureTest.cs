using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class PlayCreatureTest
  {
    static readonly LogosCreatureCard PlayedCard = new LogosCreatureCard();

    static readonly Card[] otherCards = new[]
      {new LogosCreatureCard(), new LogosCreatureCard(), new LogosCreatureCard()};

    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void Resolve_EmptyBoard(Player playingPlayer)
    {
      var state = TestState(playingPlayer);
      var sut = new PlayCreature(
        PlayedCard,
        0);

      sut.Resolve(state);

      var expectedState = TestState(playingPlayer);
      var expectedCreature = new Creature(PlayedCard.Power, PlayedCard.Armor, PlayedCard);
      expectedState.Fields[playingPlayer].Add(expectedCreature);
      expectedState.Hands[playingPlayer].Remove(PlayedCard);
      expectedState.ResolvedEffects.Add(new CreaturePlayed(expectedCreature, 0));
      Assert.AreEqual(expectedState, state);
    }

    [TestCase(-1)]
    [TestCase(1)]
    public void Resolve_EmptyBoard_InvalidPosition(int position)
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new PlayCreature(
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

    [Test]
    public void Resolve_EmptyBoard_CardNotPresentInHand()
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new PlayCreature(
        PlayedCard,
        0);

      try
      {
        sut.Resolve(state);
      }
      catch (CardNotPresentException)
      {
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
      var sut = new PlayCreature(PlayedCard, position);

      sut.Resolve(state);

      var expectedState = StateWithTwoCreatures(playingPlayer);
      var expectedCreature = new Creature(PlayedCard.Power, PlayedCard.Armor, PlayedCard);
      expectedState.Fields[playingPlayer].Insert(position, expectedCreature);
      expectedState.Hands[playingPlayer].Remove(PlayedCard);
      expectedState.ResolvedEffects.Add(new CreaturePlayed(expectedCreature, position));
      Assert.AreEqual(expectedState, state);
    }

    [TestCase(-1)]
    [TestCase(3)]
    public void Resolve_TwoCreaturesOnBoard_InvalidPosition(int position)
    {
      var state = StateWithTwoCreatures(Player.Player2);
      var sut = new PlayCreature(PlayedCard, position);

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

    static MutableState TestState(Player playingPlayer)
    {
      var hands = new Dictionary<Player, ISet<Card>>
      {
        {playingPlayer, new HashSet<Card> {PlayedCard, otherCards[0]}},
        {playingPlayer.Other(), new HashSet<Card> {otherCards[1], otherCards[2]}}
      };
      return StateTestUtil.EmptyMutableState.New(playingPlayer, hands: hands);
    }

    static MutableState StateWithTwoCreatures(Player playingPlayer)
    {
      var creature1 = new Creature(1, 1, null);
      var creature2 = new Creature(2, 2, null);

      var state = TestState(playingPlayer);
      state.Fields[Player.Player2] = new List<Creature> {creature1, creature2};
      return state;
    }
  }
}