using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  sealed class TargetSingleCardTest
  {
    static ICard[] playerOneDiscardCards = {new SampleActionCard(id: $"{Player.Player1}0"), new SampleActionCard(id: $"{Player.Player1}1")};
    static ICard[] playerTwoDiscardCards = {new SampleActionCard(id: $"{Player.Player2}0"), new SampleActionCard(id: $"{Player.Player2}1")};
    
    [Test]
    public void Resolve_NoValidTargets()
    {
      var state = Setup();
      bool effectResolved = false;
      EffectOnTarget effect = (_, _) => effectResolved = true;
      ValidOn validOn = (_, _) => false;
      var sut = new TargetSingleDiscardedCard(effect, validOn);

      sut.Resolve(state);

      Assert.False(effectResolved);
      StateAsserter.StateEquals(Setup(), state);
    }
    
    [Test]
    public void Resolve_AllValidTargets(
      [Values(Player.Player1, Player.Player2)]
      Player playerTurn)
    {
      var state = Setup(playerTurn);
      bool effectResolved = false;
      EffectOnTarget effect = (_, _) => effectResolved = true;
      var sut = new TargetSingleDiscardedCard(effect, Delegates.All);

      sut.Resolve(state);

      var expectedActionGroup = new SingleTargetGroup(
        effect,
        playerTurn.IsPlayer1() ?
          playerTwoDiscardCards.Concat(playerOneDiscardCards).Cast<IIdentifiable>().ToImmutableList() :
          playerOneDiscardCards.Concat(playerTwoDiscardCards).Cast<IIdentifiable>().ToImmutableList());

      Assert.False(effectResolved);
      StateAsserter.StateEquals(Setup(playerTurn).New(actionGroups: new LazyList<IActionGroup>{expectedActionGroup}), state);
    }


    [Test]
    public void Resolve_SingleValidTarget(
      [Values(Player.Player1, Player.Player2)] Player playerCard,
      [Range(0, 1)] int i)
    {
      var state = Setup();
      IIdentifiable cardTargeted = null;
      EffectOnTarget effect = (_, t) => cardTargeted = t;
      var targetCard = $"{playerCard}{i}";
      var sut = new TargetSingleDiscardedCard(effect, (s, t) => t.Id == targetCard);
      
      sut.Resolve(state);

      Assert.That(cardTargeted.Id, Is.EqualTo(targetCard));
      StateAsserter.StateEquals(Setup(), state);
    }

    MutableState Setup(Player playerTurn = Player.Player1)
    {
      var discards =
        TestUtil.Sets<ICard>(playerOneDiscardCards, playerTwoDiscardCards);
      return StateTestUtil.EmptyState.New(playerTurn: playerTurn, discards: discards);
    }
  }
}