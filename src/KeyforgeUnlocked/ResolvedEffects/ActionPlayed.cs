﻿using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class ActionPlayed : ResolvedEffectWithCard
  {
    public ActionPlayed(Card card) : base(card)
    {
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return $"{_card.Name} played";
    }
  }
}