using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects
{
    public class InitiateGame : EffectBase<InitiateGame>
    {
        protected override void ResolveImpl(IMutableState state)
        {
            state.ShuffleDeck(Player.Player1);
            state.ShuffleDeck(Player.Player2);
            state.Effects.Enqueue(new DrawInitialHands());
            state.Effects.Enqueue(new DeclareHouse());
            state.Effects.Enqueue(new FirstTurn());
            state.Effects.Enqueue(new ReadyCardsAndRestoreArmor());
            state.Effects.Enqueue(new DrawToHandLimit());
            state.Effects.Enqueue(new EndTurn());
        }
    }
}