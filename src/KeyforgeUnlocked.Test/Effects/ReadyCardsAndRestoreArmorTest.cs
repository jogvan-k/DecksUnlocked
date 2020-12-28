using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Effects
{
  [TestFixture]
  class ReadyCardsAndRestoreArmorTest
  {
    readonly ReadyCardsAndRestoreArmor _sut = new ReadyCardsAndRestoreArmor();

    [Test]
    public void Resolve_EmptyState()
    {
      var state = StateTestUtil.EmptyMutableState;

      _sut.Resolve(state);

      var expectedState = StateTestUtil.EmptyState;
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void Resolve_ReadyAndUnreadyCreaturesWithBrokenArmor(
      [Values(Player.Player1, Player.Player2)]Player playerTurn)
    {
      var playerCreatureCard1 = new SampleCreatureCard(armor: 0);
      var playerCreatureCard2 = new SampleCreatureCard(armor: 1);
      var opponentCreatureCard1 = new SampleCreatureCard(armor: 2);
      var opponentCreatureCard2 = new SampleCreatureCard(armor: 3);
      var fields = TestUtil.Lists(
        new[]
        {
          new Creature(playerCreatureCard1, isReady: true),
          new Creature(playerCreatureCard2, isReady: false, brokenArmor: 1)
        }.AsEnumerable(), new[]
        {
          new Creature(opponentCreatureCard1, isReady: true, brokenArmor: 2),
          new Creature(opponentCreatureCard2, isReady: false, brokenArmor: 2)
        });
      var state = StateTestUtil.EmptyMutableState.New(playerTurn, fields: fields);

      _sut.Resolve(state);

      var expectedFields = TestUtil.Lists(
        new[]
        {
          new Creature(playerCreatureCard1, isReady: true),
          new Creature(playerCreatureCard2, isReady: playerTurn.IsPlayer1())
        }.AsEnumerable(), new[]
        {
          new Creature(opponentCreatureCard1, isReady: true),
          new Creature(opponentCreatureCard2, isReady: playerTurn.IsPlayer2())
        });

      var expectedResolvedEffects = new List<IResolvedEffect>
        {new CreatureReadied(new Creature(playerTurn.IsPlayer1() ? playerCreatureCard2 : opponentCreatureCard2, isReady: true))};
      var expectedState = StateTestUtil.EmptyState.New(playerTurn, fields: expectedFields,
        resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects));
      StateAsserter.StateEquals(expectedState, state);
    }

    [Test]
    public void Resolve_ReadyAndUnreadyArtifacts(
      [Values(Player.Player1, Player.Player2)] Player playerTurn)
    {
      var player1Artifact1 = new SampleArtifactCard();
      var player1Artifact2 = new SampleArtifactCard();
      var player2Artifact1 = new SampleArtifactCard();
      var player2Artifact2 = new SampleArtifactCard();

      var artifacts = TestUtil.Sets(
        new[] {new Artifact(player1Artifact1, false), new Artifact(player1Artifact2, true)}.AsEnumerable(),
        new[] {new Artifact(player2Artifact1, true), new Artifact(player2Artifact2, false)}.AsEnumerable()
      );

      var state = StateTestUtil.EmptyMutableState.New(playerTurn, artifacts: artifacts);
      
      _sut.Resolve(state);

      var expectedArtifacts = TestUtil.Sets(new []{new Artifact(player1Artifact1, playerTurn.IsPlayer1()), new Artifact(player1Artifact2, true)}.AsEnumerable(),
        new []{new Artifact(player2Artifact1, true), new Artifact(player2Artifact2, playerTurn.IsPlayer2())}.AsEnumerable());
      
      var expectedResolvedEffects = new List<IResolvedEffect>
        {new ArtifactReadied(playerTurn.IsPlayer1() ? player1Artifact1 : player2Artifact2)};
      var expectedState = StateTestUtil.EmptyState.New(playerTurn, artifacts: expectedArtifacts,
        resolvedEffects: new LazyList<IResolvedEffect>(expectedResolvedEffects));
      StateAsserter.StateEquals(expectedState, state);
    }
  }
}