using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using NUnit.Framework;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  public class PlayCreatureTest
  {
    static readonly SimpleCreatureCard PlayedCard = new SimpleCreatureCard();

    [TestCase(Player.Player1)]
    [TestCase(Player.Player2)]
    public void Resolve_EmptyBoard(Player playingPlayer)
    {
      var state = TestState(playingPlayer);
      var sut = new PlayCreature(
        playingPlayer,
        PlayedCard,
        0);

      sut.Resolve(state);

      AssertFields(playingPlayer, state);
      AssertHandsAndDiscards(playingPlayer, state);
    }

    [TestCase(-1)]
    [TestCase(1)]
    public void Resolve_EmptyBoard_InvalidPosition(int position)
    {
      var state = TestUtil.EmptyMutableState;
      var sut = new PlayCreature(
        Player.Player1,
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
      var state = TestUtil.EmptyMutableState;
      var sut = new PlayCreature(
        Player.Player1,
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
      var state = StateWithTwoCreatures(playingPlayer, out var creature1, out var creature2);
      var sut = new PlayCreature(playingPlayer, PlayedCard, position);

      sut.Resolve(state);

      Assert.True(state.Fields[playingPlayer.Other()].Count == 0);
      var field = state.Fields[playingPlayer];
      Assert.True(HasCreaturesSameStats(PlayedCard.InsantiateCreature(), field[position]));
      field.Remove(field[position]);
      Assert.AreEqual(creature1, field[0]);
      Assert.AreEqual(creature2, field[1]);
      AssertHandsAndDiscards(playingPlayer, state);
    }

    [TestCase(-1)]
    [TestCase(3)]
    public void Resolve_TwoCreaturesOnBoard_InvalidPosition(int position)
    {
      var state = StateWithTwoCreatures(Player.Player2, out _, out _);
      var sut = new PlayCreature(Player.Player2, PlayedCard, position);

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
        {playingPlayer, new HashSet<Card> {PlayedCard, new SimpleCreatureCard()}},
        {playingPlayer.Other(), new HashSet<Card> {new SimpleCreatureCard(), new SimpleCreatureCard()}}
      };
      return TestUtil.EmptyMutableState.New(hands: hands);
    }

    static MutableState StateWithTwoCreatures(Player playingPlayer,
      out Creature creature1,
      out Creature creature2)
    {
      creature1 = new Creature(1, 1, null);
      creature2 = new Creature(2, 2, null);

      var state = TestState(playingPlayer);
      state.Fields[Player.Player2] = new List<Creature> {creature1, creature2};
      return state;
    }

    static void AssertFields(Player playingPlayer,
      MutableState state)
    {
      Assert.True(state.Fields[playingPlayer.Other()].Count == 0);
      var creatureOnField = state.Fields[playingPlayer].Single();
      Assert.True(
        HasCreaturesSameStats(
          PlayedCard.InsantiateCreature(),
          creatureOnField));
      Assert.AreEqual(PlayedCard, creatureOnField.Card);
    }

    static void AssertHandsAndDiscards(Player playingPlayer,
      MutableState state)
    {
      Assert.AreEqual(1, state.Hands[playingPlayer].Count);
      Assert.AreEqual(2, state.Hands[playingPlayer.Other()].Count);
      Assert.AreEqual(0, state.Discards[playingPlayer].Count);
      Assert.AreEqual(0, state.Discards[playingPlayer.Other()].Count);
    }

    static bool HasCreaturesSameStats(Creature creature1,
      Creature creature2)
    {
      return creature1.BasePower == creature2.BasePower
             && creature1.Armor == creature2.Armor
             && creature1.Damage == creature2.Damage;
    }
  }
}