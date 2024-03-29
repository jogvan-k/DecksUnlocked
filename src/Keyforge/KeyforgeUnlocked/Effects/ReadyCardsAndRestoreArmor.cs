using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
    public sealed class ReadyCardsAndRestoreArmor : EffectBase<ReadyCardsAndRestoreArmor>
    {
        protected override void ResolveImpl(IMutableState state)
        {
            var field = state.Fields[state.PlayerTurn];
            for (int i = 0; i < field.Count; i++)
            {
                var creature = field[i];
                if (!creature.IsReady)
                {
                    creature.IsReady = true;
                    state.ResolvedEffects.Add(new CreatureReadied(creature));
                }

                creature.BrokenArmor = 0;
                field[i] = creature;
            }

            field = state.Fields[state.PlayerTurn.Other()];
            for (int i = 0; i < field.Count; i++)
            {
                var creature = field[i];
                creature.BrokenArmor = 0;
                field[i] = creature;
            }

            foreach (var artifact in state.Artifacts[state.PlayerTurn])
            {
                if (!artifact.IsReady)
                {
                    var a = artifact;
                    a.IsReady = true;
                    state.Artifacts[state.PlayerTurn].Remove(artifact);
                    state.Artifacts[state.PlayerTurn].Add(a);
                    state.ResolvedEffects.Add(new ArtifactReadied(a));
                }
            }
        }
    }
}