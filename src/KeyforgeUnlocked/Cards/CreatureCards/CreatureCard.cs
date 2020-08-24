using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.CreatureCards
{
  public abstract class CreatureCard : Card
  {
    public int Power;
    public int Armor;
    public Keyword[] Keywords;

    public CreatureType[] Types;
    public Callback FightAbility;
    public Callback AfterKillAbility;
    public Callback CreatureAbility;
    public readonly Callback ReapAbility;
    public Callback DestroyedAbility;

    protected CreatureCard(
      House house,
      int power,
      int armor,
      CreatureType[] types = null,
      Keyword[] keywords = null,
      Callback playAbility = null,
      Callback fightAbility = null,
      Callback afterKillAbility = null,
      Callback creatureAbility = null,
      Callback reapAbility = null,
      Callback destroyedAbility = null) : base(house, CardType.Creature, playAbility)
    {
      Power = power;
      Armor = armor;
      Keywords = keywords ?? new Keyword[0];
      Types = types ?? new CreatureType[0];
      FightAbility = fightAbility;
      AfterKillAbility = afterKillAbility;
      CreatureAbility = creatureAbility;
      ReapAbility = reapAbility;
      DestroyedAbility = destroyedAbility;
    }

    public Creature InsantiateCreature()
    {
      return new Creature(this);
    }
  }
}