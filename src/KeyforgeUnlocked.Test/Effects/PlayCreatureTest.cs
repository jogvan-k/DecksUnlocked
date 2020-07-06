using System.Collections.Generic;
using System.Linq;
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
    SimpleCreatureCard _creatureCard = new SimpleCreatureCard();

    [Test]
    public void Resolve_EmptyBoard()
    {
      var state = TestUtil.EmptyMutableState;
      var sut = new PlayCreature(
        Player.Player1,
        _creatureCard,
        0);

      sut.Resolve(state);

      Assert.True(state.Fields[Player.Player2].Count == 0);
      Assert.True(
        HasCreaturesSameStats(
          _creatureCard.InsantiateCreature(),
          state.Fields[Player.Player1].Single()));
    }

    [TestCase(-1)]
    [TestCase(1)]
    public void Resolve_EmptyBoard_InvalidPosition(int position)
    {
      var state = TestUtil.EmptyMutableState;
      var sut = new PlayCreature(
        Player.Player1,
        _creatureCard,
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
      var state = StateWithTwoCreatures(out var creature1, out var creature2);
      var sut = new PlayCreature(Player.Player2, _creatureCard, position);

      sut.Resolve(state);

      Assert.True(state.Fields[Player.Player1].Count == 0);
      var field = state.Fields[Player.Player2];
      Assert.True(HasCreaturesSameStats(_creatureCard.InsantiateCreature(), field[position]));
      field.Remove(field[position]);
      Assert.AreEqual(creature1, field[0]);
      Assert.AreEqual(creature2, field[1]);
    }

    [TestCase(-1)]
    [TestCase(3)]
    public void Resolve_TwoCreaturesOnBoard_InvalidPosition(int position)
    {
      var state = StateWithTwoCreatures(out var creature1, out var creature2);
      var sut = new PlayCreature(Player.Player2, _creatureCard, position);

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

    static MutableState StateWithTwoCreatures(out Creature creature1,
      out Creature creature2)
    {
      creature1 = new Creature(1, 1, null);
      creature2 = new Creature(2, 2, null);
      var state = TestUtil.EmptyMutableState;
      state.Fields[Player.Player2] = new List<Creature> {creature1, creature2};
      return state;
    }

    bool HasCreaturesSameStats(Creature creature1,
      Creature creature2)
    {
      return creature1.BasePower == creature2.BasePower
             && creature1.Armor == creature2.Armor
             && creature1.Damage == creature2.Damage;
    }
  }
}