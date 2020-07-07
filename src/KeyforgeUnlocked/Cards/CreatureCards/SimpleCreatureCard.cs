using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using UnlockedCore.Actions;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Cards.CreatureCards
{
  public class SimpleCreatureCard : CreatureCard
  {
    const int Power = 3;
    const int Armor = 0;
    public SimpleCreatureCard(House house = House.Logos, int power = -1) : base("SimpleCreature", house, power > 0 ? power : Power, Armor)
    {
    }

  }
}