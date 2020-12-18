﻿using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.ActionCards.Sanctum;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Cards.CreatureCards.Brobnar;
using KeyforgeUnlocked.Cards.CreatureCards.Sanctum;
using KeyforgeUnlocked.Cards.CreatureCards.Shadows;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;
using static KeyforgeUnlockedTest.States.StateField;

namespace KeyforgeUnlockedTest.States
{
  [TestFixture]
  sealed class GetHashCode
  {
    [Test, Combinatorial]
    public void GetHashCode_SameHashOnEqualFieldValues(
      [Values(None, TurnNumber, IsGameOver, PlayerTurn, ActiveHouse, Keys, Aember, StateField.ActionGroups, Decks, Hands, Discards, Archives, Fields, StateField.Effects)] StateField fieldA,
      [Values(None, TurnNumber, IsGameOver, PlayerTurn, ActiveHouse, Keys, Aember, StateField.ActionGroups, Decks, Hands, Discards, Archives, Fields, StateField.Effects)] StateField fieldB,
      [Values(Player.Player1, Player.Player2)] Player player)
    {
      var first = Construct(fieldA, player);
      var second = Construct(fieldB, player);

      // Should not be reference equals
      Assert.False(ReferenceEquals(first, second));

      if (fieldA.Equals(fieldB))
      {
        AssertEquals(first, second);
      }
      else
      {
        AssertNotEqualAndDifferentHash(first, second);
      }
    }

    [Test, Combinatorial]
    public void GetHashCode_SameHashOnDifferentFieldValues(
      [Values(None, PreviousState)] StateField fieldA,
      [Values(None, PreviousState)] StateField fieldB)
    {
      var first = Construct(fieldA, Player.Player1);
      var second = Construct(fieldB, Player.Player1);

      // Should not be reference equals
      Assert.False(ReferenceEquals(first, second));

      AssertEquals(first, second);
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

    static ImmutableState Construct(StateField field, Player player)
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
        case PreviousState:
          state.PreviousState = StateTestUtil.EmptyState;
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
        case Fields:
          state.Fields[player].Add(new Creature(_sampleCard));
          break;
        case StateField.Effects:
          state.Effects.Enqueue(new TryForge());
          break;
        default:
          throw new InvalidOperationException($"Field {field} not supported.");
      }

      return state.ToImmutable();
    }

    static MutableState BeginningState()
    {
      var state = StateTestUtil.EmptyMutableState;
      state.playerTurn = Player.Player1;
      state.TurnNumber = 2;
      state.IsGameOver = false;
      state.PreviousState = null;
      state.ActiveHouse = House.Brobnar;
      state.Keys = TestUtil.Ints(1, 1);
      state.Aember = TestUtil.Ints(1, 1);
      state.ActionGroups = new LazyList<IActionGroup>(new[] {new EndTurnGroup()});
      state.Decks = TestUtil.Stacks(_player1Deck, _player2Deck);
      state.Hands = TestUtil.Sets(_player1Hand, _player2Hand);
      state.Discards = TestUtil.Sets(_player1Discard, _player2Discard);
      state.Archives = TestUtil.Sets(_player1Archives,_player2Archives);
      state.Effects = new LazyStackQueue<IEffect>(new[] {(IEffect) new DrawInitialHands(), new DeclareHouse()});
      state.Fields = TestUtil.Lists(new Creature(_player1Field), new Creature(_player2Field));
      return state;
    }
    
    static CreatureCard _sampleCard = new Krump();
    
    static IEnumerable<Card> _player1Deck = new[] {(Card) new Umbra(), new Smaaash(), new Francus()};
    static IEnumerable<Card> _player2Deck = new[] {(Card) new Umbra(), new Smaaash(), new Francus()};

    static IEnumerable<Card> _player1Hand = new[] {(Card) new MacisAsp(), new Smaaash()};
    static IEnumerable<Card> _player2Hand = new[] {(Card) new MacisAsp(), new Smaaash()};

    static Card _player1Discard = new Inspiration();
    static Card _player2Discard = new Inspiration();

    static IEnumerable<Card> _player1Archives = new[] {(Card) new BadPenny(), new Umbra()};
    static IEnumerable<Card> _player2Archives = new[] {(Card) new BadPenny(), new Umbra()};
    
    static CreatureCard _player1Field = new NoddyTheThief();
    static CreatureCard _player2Field = new NoddyTheThief();
  }

  enum StateField
  {
    None,
    TurnNumber,
    IsGameOver,
    PreviousState,
    PlayerTurn,
    ActiveHouse,
    Keys,
    Aember,
    ActionGroups,
    Decks,
    Hands,
    Discards,
    Archives,
    Fields,
    Effects
  }
}