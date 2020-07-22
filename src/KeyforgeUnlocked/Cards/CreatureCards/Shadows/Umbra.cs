namespace KeyforgeUnlocked.Cards.CreatureCards.Shadows
{
  public sealed class Umbra : CreatureCard
  {
    const string name = "Umbra";
    const int power = 2;
    const int armor = 0;
    static Keyword[] keywords = {Keyword.Skirmish};

    public Umbra(House house = House.Shadows) : base(
      name, house, power,
      armor, keywords)
    {
    }
  }
}