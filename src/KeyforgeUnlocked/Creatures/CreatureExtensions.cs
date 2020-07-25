namespace KeyforgeUnlocked.Creatures
{
  public static class CreatureExtensions
  {
    public static Creature Damage(this Creature creature, int damage)
    {
      creature.Damage += damage;
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
  }
}