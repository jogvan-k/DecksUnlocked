using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlocked.Types.HistoricData.Extensions;

namespace KeyforgeUnlocked.Effects
{
    public sealed class EndTurn : EffectBase<EndTurn>
    {
        protected override void ResolveImpl(IMutableState state)
        {
            state.RaiseEvent(EventType.TurnEnded);
            state.PlayerTurn = state.PlayerTurn.Other();
            state.TurnNumber++;
            state.ActiveHouse = null;
            state.HistoricData.NextTurn();
            state.ResolvedEffects.Add(new TurnEnded());
            state.Effects.Enqueue(new CheckGameTurnLimit());
            state.Effects.Enqueue(new TryForge());
            state.Effects.Enqueue(new DeclareHouse());
        }
    }
}