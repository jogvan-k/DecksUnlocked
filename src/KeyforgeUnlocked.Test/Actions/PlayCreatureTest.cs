using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using PlayCreature = KeyforgeUnlocked.Actions.PlayCreature;

namespace KeyforgeUnlockedTest.Actions
{
  [TestFixture]
  class PlayCreatureTest : ActionTestBase
  {
    static readonly CreatureCard Card = new SampleCreatureCard(house: House.Logos);

    [Test]
    public void Act_EmptyBoard()
    {
      var state = StateTestUtil.EmptyMutableState;
      var sut = new PlayCreature(Card, 0);

      var expectedEffects = new StackQueue<IEffect>();
      expectedEffects.Enqueue(new KeyforgeUnlocked.Effects.PlayCreature(Card, 0));
      var expectedState = StateTestUtil.EmptyMutableState.New(effects: expectedEffects);

      Act(sut, state, expectedState);
    }
  }
}