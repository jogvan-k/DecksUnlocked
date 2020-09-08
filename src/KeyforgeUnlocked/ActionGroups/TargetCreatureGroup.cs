using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class TargetCreatureGroup : ResolveEffectActionGroup
  {
    private readonly EffectOnCreature _effect;
    private readonly IMutableList<Creature> _targets;

    public TargetCreatureGroup(EffectOnCreature effect, IMutableList<Creature> targets) : base(ActionType.TargetCreature)
    {
      _effect = effect;
      _targets = targets;
    }
    internal TargetCreatureGroup(IEnumerable<Action> actions) : base(ActionType.TargetCreature)
    {
    }

    protected override IImmutableList<Action> InitiateActions(ImmutableState origin)
    {
      return _targets.Select(t => (Action) new TargetCreature(origin, _effect, t)).ToImmutableList();
    }
  }
}