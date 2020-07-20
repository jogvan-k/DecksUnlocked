using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlockedTest.Util
{
  public static class CreatureTestUtil
  {
    static CreatureCard sampleLogosCreatureCard = new LogosCreatureCard();
    static CreatureCard sampleUntamedCreatureCard = new UntamedCreatureCard();
    static CreatureCard sampleStarAllianceCreatureCard = new StarAllianceCreatureCard();

    public static Creature SampleLogosCreature(string creatureId,
      bool isReady = true)
    {
      return Creature(sampleLogosCreatureCard, creatureId, isReady);
    }

    public static Creature SampleUntamedCreature(string creatureId,
      bool isReady = true)
    {
      return Creature(sampleUntamedCreatureCard, creatureId, isReady);
    }

    public static Creature SampleStarAllianceCreature(string creatureId,
      bool isReady = true)
    {
      return Creature(sampleStarAllianceCreatureCard, creatureId, isReady);
    }

    static Creature Creature(CreatureCard card,
      string creatureId,
      bool isReady)
    {
      return new Creature(
        creatureId, card.Power, 0,
        card, 0, 0,
        isReady);
    }
  }
}