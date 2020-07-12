using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;
using NUnit.Framework;
using PlayCreature = KeyforgeUnlocked.Actions.PlayCreature;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class PlayCreatureTest
  {
    static readonly CreatureCard Card = new SimpleCreatureCard();

    [Test]
    public void DoActionNoResolve_EmptyBoard()
    {
      var state = TestUtil.EmptyMutableState;
      var sut = new PlayCreature(Card, 0);

      state = sut.DoActionNoResolve(state);

      var expectedEffects = new Queue<IEffect>();
      expectedEffects.Enqueue(new KeyforgeUnlocked.Effects.PlayCreature(Card, 0));
      var expectedState = TestUtil.EmptyMutableState.New(effects: expectedEffects);
      Assert.AreEqual(expectedState, state);

      var effect = (KeyforgeUnlocked.Effects.PlayCreature) state.Effects.Single();
      Assert.AreEqual(Card, effect.Card);
      Assert.AreEqual(0, effect.Position);
    }
  }
}