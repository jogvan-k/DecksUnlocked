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
    public static readonly EffectOnCreature StunCreature = (s, t) =>
    {
      if (t.IsStunned())
        return;
      var creature = t;
      creature.State = t.State | CreatureState.Stunned;
      s.SetCreature(creature);
      s.ResolvedEffects.Add(new CreatureStunned(creature));
    };

    public static EffectOnCreature SwapCreatures(string c) =>
      (s, t) =>
      {
        s.SwapCreatures(c, t.Id);
        s.ResolvedEffects.Add(new CreaturesSwapped(s.FindCreature(c, out _), t));
      };

    public static readonly ValidOn All = (s, c) => true;

    public static ValidOn AlliesOf(string c) => (s, t) =>
    {
      if (c == t.Id)
        return false;
      s.FindCreature(c, out var cControllingPlayer);
      s.FindCreature(t.Id, out var tControllingPlayer);
      return cControllingPlayer == tControllingPlayer;
    };

    public static Callback NoChange => (state, id) => { };
  }
}