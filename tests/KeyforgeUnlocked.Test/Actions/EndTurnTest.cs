using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;
using EndTurn = KeyforgeUnlocked.Actions.EndTurn;

namespace KeyforgeUnlockedTest.Actions
{
    [TestFixture]
    class EndTurnTest : ActionTestBase<EndTurn>
    {
        [Test]
        public void Act_EmptyBoard()
        {
            var state = StateTestUtil.EmptyMutableState;
            var sut = new EndTurn(null);

            var expectedEffects = new LazyStackQueue<IEffect>();
            expectedEffects.Enqueue(new ReadyCardsAndRestoreArmor());
            expectedEffects.Enqueue(new DrawToHandLimit());
            expectedEffects.Enqueue(new KeyforgeUnlocked.Effects.EndTurn());
            var expectedState = StateTestUtil.EmptyMutableState.New(effects: expectedEffects);
            expectedState.HistoricData.ActionPlayedThisTurn = true;

            ActAndAssert(sut, state, expectedState);
        }
    }
}