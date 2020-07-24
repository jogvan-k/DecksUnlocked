using System;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlockedTest.Effects;

namespace KeyforgeUnlockedConsole.ConsoleExtensions
{
  public static class ResolvedEffectsExtensions
  {
    public static string ToConsole(this IResolvedEffect effect)
    {
      switch (effect)
      {
        case CreaturePlayed e:
          return e.ToConsole();
        case CardsDrawn e:
          return e.ToConsole();
        case CardDiscarded e:
          return e.ToConsole();
        case TurnEnded e:
          return e.ToConsole();
        case Reaped e:
          return e.ToConsole();
        case KeyForged e:
          return e.ToConsole();
        case HouseDeclared e:
          return e.ToConsole();
        case CreatureFought e:
          return e.ToConsole();
        case CreatureDied e:
          return e.ToConsole();
        case AemberStolen e:
          return e.ToConsole();
        case CardReturnedToHand e:
          return e.ToConsole();
        case CreatureStunned e:
          return e.ToConsole();
        case StunRemoved e:
          return e.ToConsole();
        default:
          throw new NotImplementedException();
      }
    }

    static string ToConsole(this CreaturePlayed effect)
    {
      return $"Played creature {effect.Creature.Card.Name} on position {effect.Position}";
    }

    static string ToConsole(this CardsDrawn effect)
    {
      return $"{effect.NoDrawn} cards drawn";
    }

    static string ToConsole(this CardDiscarded effect)
    {
      return $"Discarded {effect.Card}";
    }

    static string ToConsole(this TurnEnded effect)
    {
      return $"Turn ended";
    }

    static string ToConsole(this Reaped effect)
    {
      return $"{effect.Creature.Card.Name} reaped";
    }

    static string ToConsole(this KeyForged effect)
    {
      return $"Key forged for {effect.KeyCost} Æmber";
    }

    static string ToConsole(this HouseDeclared effect)
    {
      return $"House {effect.House} declared.";
    }

    static string ToConsole(this CreatureFought effect)
    {
      return $"{effect.Fighter.Card.Name} (power: {effect.Fighter.Power}) attacked {effect.Target.Card.GetType().Name} (power: {effect.Target.Power})";
    }

    static string ToConsole(this CreatureDied effect)
    {
      return $"{effect.Creature.Card.Name} died";
    }

    static string ToConsole(this AemberStolen effect)
    {
      return $"{effect.stealingPlayer} stole {effect.stolenAmount} aember";
    }

    static string ToConsole(this CardReturnedToHand effect)
    {
      return $"{effect.card.Name} returned to hand";
    }

    static string ToConsole(this CreatureStunned effect)
    {
      return $"{effect.Creature.Card.Name} stunned";
    }

    static string ToConsole(this StunRemoved effect)
    {
      return $"Stun removed from {effect.Creature.Card.Name}";
    }
  }
}