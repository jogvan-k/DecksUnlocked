using System.Collections.Generic;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlockedTest.Effects
{
    [TestFixture]
    public class TryForgeTest
    {
        readonly TryForge _sut = new TryForge();

        [Test]
        public void Resolve_WithAember([Range(0, 7)] int aember)
        {
            var startAember = new Dictionary<Player, int> { { Player.Player1, aember }, { Player.Player2, aember } }
                .ToLookup();
            var keyForgedPlayerEventRaised = Player.None;
            var events = new LazyEvents();
            events.Subscribe(new Identifiable(""), EventType.KeyForged, (_, _, p) =>
                keyForgedPlayerEventRaised = p);
            var state = StateTestUtil.EmptyState.New(aember: startAember, events: events);

            _sut.Resolve(state);

            IState expectedstate;
            if (aember < DefaultForgeCost)
            {
                expectedstate = StateTestUtil.EmptyState.New(aember: startAember, events: events);
                Assert.That(keyForgedPlayerEventRaised, Is.EqualTo(Player.None));
            }
            else
            {
                var expectedKeys =
                    new Dictionary<Player, int> { { Player.Player1, 1 }, { Player.Player2, 0 } }.ToLookup();
                var expectedAembers = new Dictionary<Player, int>
                    { { Player.Player1, aember - DefaultForgeCost }, { Player.Player2, aember } }.ToLookup();
                var expectedResolvedEffects = new List<IResolvedEffect>
                    { new KeyForged(Player.Player1, DefaultForgeCost) };
                expectedstate = StateTestUtil.EmptyState.New(
                    keys: expectedKeys, aember: expectedAembers, events: events,
                    resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects));
                Assert.That(keyForgedPlayerEventRaised, Is.EqualTo(Player.Player1));
            }

            StateAsserter.StateEquals(expectedstate, state);
        }

        [TestCase(Player.Player1)]
        [TestCase(Player.Player2)]
        public void Resolve_ForgeToWinGame(Player winningPlayer)
        {
            var startKeys = new Dictionary<Player, int>
            {
                { winningPlayer, KeysRequiredToWin - 1 },
                { winningPlayer.Other(), 0 }
            }.ToLookup();
            var startAember = new Dictionary<Player, int>
            {
                { winningPlayer, DefaultForgeCost },
                { winningPlayer.Other(), 0 }
            }.ToLookup();
            var keyForgedPlayerEventRaised = Player.None;
            var events = new LazyEvents();
            events.Subscribe(new Identifiable(""), EventType.KeyForged, (_, _, p) =>
                keyForgedPlayerEventRaised = p);
            var state = StateTestUtil.EmptyState.New(playerTurn: winningPlayer, keys: startKeys, aember: startAember,
                events: events);

            _sut.Resolve(state);

            var expectedKeys = new Dictionary<Player, int>
                { { winningPlayer, KeysRequiredToWin }, { winningPlayer.Other(), 0 } }.ToLookup();
            var expectedResolvedEffects = new List<IResolvedEffect> { new KeyForged(winningPlayer, DefaultForgeCost) };
            var expectedState = StateTestUtil.EmptyState.New(playerTurn: winningPlayer, keys: expectedKeys,
                isGameOver: true, events: events,
                resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects));
            StateAsserter.StateEquals(expectedState, state);
            Assert.That(keyForgedPlayerEventRaised, Is.EqualTo(winningPlayer));
        }
    }
}