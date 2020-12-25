using System;
using System.Linq;

namespace KeyforgeUnlocked.Creatures
{
  public static class CreatureExtensions
  {
    public static Creature Damage(this Creature creature, int damage)
    {
      var brokenArmor = Math.Min(creature.Armor, damage);
      creature.BrokenArmor += brokenArmor;
      creature.Damage += damage - brokenArmor;
      return creature;
    }

    public static Creature Heal(this Creature creature, int amount, out int healedAmount)
    {
      healedAmount = Math.Min(creature.Damage, amount);
      creature.Damage -= healedAmount;
      return creature;
    }
    public static bool IsStunned(this Creature creature)
    {
      return (creature.State & CreatureState.Stunned) != 0;
    }

    public static bool IsWarded(this Creature creature)
    {
      return (creature.State & CreatureState.Warded) != 0;
    }

    public static bool IsEnraged(this Creature creature)
    {
      return (creature.State & CreatureState.Enraged) != 0;
    }
    
    public static bool HasTaunt(this Creature creature)
    {
      return creature.Card.CardKeywords.Contains(Keyword.Taunt);
    }
  }
}