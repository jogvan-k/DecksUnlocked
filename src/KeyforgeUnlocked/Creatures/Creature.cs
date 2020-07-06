using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Creatures
{
  public class Creature
  {
    public int BasePower { get; }
    public int Armor { get; }
    public CreatureCard Card { get; }
    public int PowerCounters { get; }
    public int Power => BasePower + PowerCounters;
    public int Damage { get; }
    public int Health => Power - Damage;

    public Creature(int basePower, int armor, CreatureCard card)
    {
      BasePower = basePower;
      Armor = armor;
      Card = card;
      PowerCounters = 0;
      Damage = 0;
    }
  }
}