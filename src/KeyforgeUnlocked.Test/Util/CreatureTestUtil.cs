using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlockedTest.Util
{
  public static class CreatureTestUtil
  {
    static CreatureCard sampleLogosCreatureCard = new LogosCreatureCard();

    public static Creature SampleLogosCreature(string creatureId,
      bool isReady)
    {
      return new Creature(
        creatureId, 1, 0,
        sampleLogosCreatureCard, 0, 0,
        isReady);
    }
  }
}