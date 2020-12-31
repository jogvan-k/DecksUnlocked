using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using UnlockedCore;

namespace KeyforgeUnlocked.Types
{
  public delegate void Callback(IMutableState state, IIdentifiable target, Player owningPlayer);
  public delegate int Modifier(IState state);
  public delegate bool ValidOn(IState state, IIdentifiable id);

  public static class Delegates
  {
    public static readonly Callback StunTarget = (s, t, _) =>
    {
      s.StunCreature(t);
    };

    public static Callback ReadyAndUse(UseCreature allowedUsage = UseCreature.All) => (s, c,_) => s.AddEffect(new ReadyCreatureAndUse(c, true, allowedUsage));

    public static readonly Callback ReturnTargetToHand = (s, c, _) => s.ReturnCreatureToHand(c);

    public static Callback SwapCreatures(IIdentifiable c) =>
      (s, t, _) =>
      {
        s.SwapCreatures(c, t);
      };

    public static readonly ValidOn All = (_, _) => true;
    
    public static ValidOn AllExcept(IIdentifiable e) => (_, t) => !t.Equals(e);

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

    public static Callback NoChange => (_, _, _) => { };
  }
}