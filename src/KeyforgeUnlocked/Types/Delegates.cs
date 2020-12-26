using System;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using UnlockedCore;

namespace KeyforgeUnlocked.Types
{
  public delegate void Callback(MutableState state, IIdentifiable invoker);

  public delegate void EffectOnTarget(MutableState state, IIdentifiable target);

  public delegate bool ValidOn(IState state, IIdentifiable id);

  public static class Delegates
  {
    public static readonly EffectOnTarget StunTarget = (s, t) =>
    {
      s.StunCreature(t);
    };

    public static readonly EffectOnTarget ReturnTargetToHand = (s, c) => s.ReturnCreatureToHand(c);

    public static EffectOnTarget SwapCreatures(IIdentifiable c) =>
      (s, t) =>
      {
        s.SwapCreatures(c, t);
      };

    public static readonly ValidOn All = (_, _) => true;
    
    public static ValidOn AllExcept(IIdentifiable e) => (_, t) => !t.Equals(e);

    public static ValidOn AlliesOf(IIdentifiable c) => (s, t) =>
    {
      if (c.Equals(t))
        return false;
      s.FindCreature(c, out var cControllingPlayer, out _);
      s.FindCreature(t, out var tControllingPlayer, out _);
      return cControllingPlayer == tControllingPlayer;
    };

    public static ValidOn EnemiesOf(Player player) => (s, t) =>
    {
      s.FindCreature(t, out var tControllingPlayer, out _);
      return player != tControllingPlayer;
    };

    public static ValidOn AlliesOf(Player player) => (s, t) =>
    {
      s.FindCreature(t, out var controllingPlayer, out _);
      return controllingPlayer == player;
    };

    public static ValidOn BelongingTo(Player player) => (s, t) => s.Metadata.InitialDecks[player].Contains(t);

    public static ValidOn OfHouse(House house) => (s, t) => s.FindCard(t).House == house;
    public static ValidOn IsCreatureCard() => (s, t) => t is CreatureCard;

    public static ValidOn Not(IIdentifiable c) => (_, t) => !t.Equals(c);

    public static Callback NoChange => (_, _) => { };
  }
}