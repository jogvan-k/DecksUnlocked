using System;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.States.Extensions
{
  public static class ResolvePipMutableStateExtensions
  {
    public static void ResolvePip(
      this MutableState state,
      Pip pip)
    {
      switch (pip)
      {
        case Pip.Aember:
          ResolveAemberPip(state);
          break;
        default:
          throw new NotImplementedException($"Pip of type {pip} is not implemented.");
      }
    }

    static void ResolveAemberPip(MutableState state)
    {
      state.GainAember();
    }
  }
}