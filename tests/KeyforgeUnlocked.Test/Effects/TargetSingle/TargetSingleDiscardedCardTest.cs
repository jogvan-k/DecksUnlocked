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

namespace KeyforgeUnlockedTest.Effects.TargetSingle
{
    [TestFixture]
    sealed class TargetSingleDiscardedCardTest
    {
        static readonly ICard[] _playerOneDiscardCards =
            { new SampleActionCard(id: $"{Player.Player1}0"), new SampleActionCard(id: $"{Player.Player1}1") };

        static readonly ICard[] _playerTwoDiscardCards =
            { new SampleActionCard(id: $"{Player.Player2}0"), new SampleActionCard(id: $"{Player.Player2}1") };

        [Test]
        public void Resolve_NoValidTargets()
        {
            var state = Setup();
            bool effectResolved = false;
            Callback effect = (_, _, _) => effectResolved = true;
            ValidOn validOn = (_, _) => false;
            var sut = new TargetSingleDiscardedCard(effect, validOn: validOn);

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
            Callback effect = (_, _, _) => effectResolved = true;
            var sut = new TargetSingleDiscardedCard(effect);

            sut.Resolve(state);

            var playerOneDiscards = _playerOneDiscardCards.Cast<IIdentifiable>().Select(d => (d, Player.Player1));
            var playerTwoDiscards = _playerTwoDiscardCards.Cast<IIdentifiable>().Select(d => (d, Player.Player2));

            var expectedActionGroup = new SingleTargetGroup(
                effect,
                playerTurn.IsPlayer1()
                    ? playerTwoDiscards.Concat(playerOneDiscards).ToImmutableList()
                    : playerOneDiscards.Concat(playerTwoDiscards).ToImmutableList());

            Assert.False(effectResolved);
            StateAsserter.StateEquals(
                Setup(playerTurn).New(actionGroups: new LazyList<IActionGroup> { expectedActionGroup }), state);
        }


        [Test]
        public void Resolve_SingleValidTarget(
            [Values(Player.Player1, Player.Player2)]
            Player playerCard,
            [Range(0, 1)] int i)
        {
            var state = Setup();
            IIdentifiable cardTargeted = null;
            Player playerTargeted = Player.None;
            Callback effect = (_, t, p) =>
            {
                cardTargeted = t;
                playerTargeted = p;
            };
            var targetCard = $"{playerCard}{i}";
            var sut = new TargetSingleDiscardedCard(effect, validOn: (_, t) => t.Id == targetCard);

            sut.Resolve(state);

            Assert.That(cardTargeted.Id, Is.EqualTo(targetCard));
            Assert.That(playerTargeted, Is.EqualTo(playerCard));
            StateAsserter.StateEquals(Setup(), state);
        }

        IMutableState Setup(Player playerTurn = Player.Player1)
        {
            var discards =
                TestUtil.Sets<ICard>(_playerOneDiscardCards, _playerTwoDiscardCards);
            return StateTestUtil.EmptyState.New(playerTurn: playerTurn, discards: discards);
        }
    }
}