using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.Creatures
{
  public interface ICreature
  {
    string CreatureId { get; }

    int BasePower { get; }

    int Armor { get; }

    CreatureCard Card { get; }

    int PowerCounters { get; }

    int Damage { get; }

    int Power { get; }

    bool IsReady { get; }

    int Health { get; }
  }
}