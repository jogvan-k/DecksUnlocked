using System;
using KeyforgeUnlocked.ResolvedEffects;

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
  }
}