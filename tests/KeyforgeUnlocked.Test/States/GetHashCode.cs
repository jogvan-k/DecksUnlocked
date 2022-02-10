using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlocked.Types.HistoricData;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;
using static KeyforgeUnlockedTest.States.StateField;

namespace KeyforgeUnlockedTest.States
{
    [TestFixture]
    sealed class GetHashCode
    {
        [Theory]
        public void GetHashCode_VaryField(StateField field)
        {
            var original = Construct(None);
            var modified = Construct(field);

            Assert.False(ReferenceEquals(original, modified));
            if (field == None)
            {
                AssertEquals(original, modified);
            }
            else
            {
                AssertNotEqualAndDifferentHash(original, modified);
            }
        }

        [Theory]
        public void GetHashCode_VaryToSameField(StateField field)
        {
            var state1 = Construct(field);
            var state2 = Construct(field);

            Assert.False(ReferenceEquals(state1, state2));
            AssertEquals(state1, state2);
        }

        [Test]
        public void GetHashCode_VaryPlayer(
            [Values(Keys, Aember, Decks, Hands, Fields, Discards, Archives, PurgedCards)]
            StateField field)
        {
            var state1 = Construct(field, Player.Player1);
            var state2 = Construct(field, Player.Player2);

            Assert.False(ReferenceEquals(state1, state2));

            AssertNotEqualAndDifferentHash(state1, state2);
        }

        static void AssertEquals(ImmutableState first, ImmutableState second)
        {
            Assert.That(first, Is.EqualTo(second));
            Assert.That(first.GetHashCode(), Is.EqualTo(second.GetHashCode()));
        }

        static void AssertNotEqualAndDifferentHash(ImmutableState first, ImmutableState second)
        {
            Assert.That(first, Is.Not.EqualTo(second));
            Assert.That(first.GetHashCode(), Is.Not.EqualTo(second.GetHashCode()));
        }

        static ImmutableState Construct(StateField field, Player player = Player.Player1)
        {
            var state = BeginningState();
            switch (field)
            {
                case None:
                    break;
                case TurnNumber:
                    state.TurnNumber += 1;
                    break;
                case IsGameOver:
                    state.IsGameOver = true;
                    break;
                case PlayerTurn:
                    state.PlayerTurn = Player.Player2;
                    break;
                case ActiveHouse:
                    state.ActiveHouse = House.Shadows;
                    break;
                case Keys:
                    state.Keys[player] += 1;
                    break;
                case Aember:
                    state.Aember[player] += 1;
                    break;
                case StateField.ActionGroups:
                    state.ActionGroups.Add(new UseCreatureGroup(state, state.Fields[player].First()));
                    break;
                case Decks:
                    state.Decks[player].Enqueue(_sampleCard);
                    break;
                case Hands:
                    state.Hands[player].Add(_sampleCard);
                    break;
                case Discards:
                    state.Discards[player].Add(_sampleCard);
                    break;
                case Archives:
                    state.Archives[player].Add(_sampleCard);
                    break;
                case PurgedCards:
                    state.PurgedCard[player].Add(_sampleCard);
                    break;
                case Fields:
                    state.Fields[player].Add(new Creature(_sampleCard));
                    break;
                case StateField.Effects:
                    state.Effects.Enqueue(new TryForge());
                    break;
                case StateField.Events:
                    state.Events.Subscribe(new Identifiable(""), EventType.TurnEnded, (_, _, _) => { });
                    break;
                case HistoricData:
                    state.HistoricData.ActionPlayedThisTurn = true;
                    break;
                default:
                    throw new InvalidOperationException($"Field {field} not supported.");
            }

            return state.ToImmutable();
        }

        static IMutableState BeginningState()
        {
            var state = StateTestUtil.EmptyMutableState;
            state.PlayerTurn = Player.Player1;
            state.TurnNumber = 2;
            state.IsGameOver = false;
            state.ActiveHouse = House.Brobnar;
            state.Keys = TestUtil.Ints(1, 1);
            state.Aember = TestUtil.Ints(1, 1);
            state.ActionGroups = new LazyList<IActionGroup>(new[] { new EndTurnGroup() });
            state.Decks = TestUtil.Stacks(_player1Deck, _player2Deck);
            state.Hands = TestUtil.Sets(_player1Hand, _player2Hand);
            state.Discards = TestUtil.Sets(_player1Discard, _player2Discard);
            state.Archives = TestUtil.Sets(_player1Archives, _player2Archives);
            state.Effects = new LazyStackQueue<IEffect>(new[] { (IEffect)new DrawInitialHands(), new DeclareHouse() });
            state.Events = new Events();
            state.HistoricData = new LazyHistoricData();
            state.Fields = TestUtil.Lists(new Creature(_player1Field), new Creature(_player2Field));
            return state;
        }

        static ICreatureCard _sampleCard = new SampleCreatureCard();

        static IEnumerable<ICard> _player1Deck = new[]
        {
            (ICard)new SampleCreatureCard(House.Shadows), new SampleCreatureCard(House.Brobnar),
            new SampleActionCard(House.Sanctum)
        };

        static IEnumerable<ICard> _player2Deck = new[]
        {
            (ICard)new SampleCreatureCard(House.Shadows), new SampleCreatureCard(House.Brobnar),
            new SampleActionCard(House.Sanctum)
        };

        static IEnumerable<ICard> _player1Hand = new[]
            { (ICard)new SampleCreatureCard(House.Dis), new SampleCreatureCard(House.Logos) };

        static IEnumerable<ICard> _player2Hand = new[]
            { (ICard)new SampleCreatureCard(House.Dis), new SampleCreatureCard(House.Logos) };

        static ICard _player1Discard = new SampleActionCard();
        static ICard _player2Discard = new SampleActionCard();

        static IEnumerable<ICard> _player1Archives = new[]
            { (ICard)new SampleActionCard(House.Mars), new SampleCreatureCard(House.Saurian) };

        static IEnumerable<ICard> _player2Archives = new[]
            { (ICard)new SampleActionCard(House.Mars), new SampleCreatureCard(House.Saurian) };

        static ICreatureCard _player1Field = new SampleCreatureCard(House.Untamed);
        static ICreatureCard _player2Field = new SampleCreatureCard(House.Untamed);
    }

    enum StateField
    {
        None,
        TurnNumber,
        IsGameOver,
        PlayerTurn,
        ActiveHouse,
        Keys,
        Aember,
        ActionGroups,
        Decks,
        Hands,
        Discards,
        Archives,
        PurgedCards,
        Fields,
        Effects,
        Events,
        HistoricData
    }
}