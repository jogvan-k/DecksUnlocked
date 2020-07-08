using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Cards.CreatureCards
{
  public class SimpleCreatureCard : CreatureCard
  {
    const int InitialPower = 3;
    const int InitialArmor = 0;
    public SimpleCreatureCard(House house = House.Logos) : base("SimpleCreature", house, InitialPower, InitialArmor)
    {
    }

  }
}