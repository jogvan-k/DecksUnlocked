using System;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Types
{
  public delegate void Callback(MutableState state, string invokerId);

  public delegate void EffectOnCreature(MutableState state, Creature target);

  public delegate bool ValidOn(IState state, Creature creature);

  public static class Delegates
  {
    public static readonly EffectOnCreature StunCreature = (s, c) =>
    {
      if (c.IsStunned())
        return;
      var creature = c;
      creature.State = c.State | CreatureState.Stunned;
      s.SetCreature(creature);
      s.ResolvedEffects.Add(new CreatureStunned(creature));
    };

    public static readonly ValidOn All = (s, c) => true;

    public static Callback NoChange => (state, id) => { };
  }
}