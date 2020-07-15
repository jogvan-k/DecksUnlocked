using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.Creatures;

namespace KeyforgeUnlockedTest.Util
{
  public static class CreatureTestUtil
  {
    static CreatureCard sampleCreatureCard = new SimpleCreatureCard();

    public static Creature SampleCreature(string creatureId,
      bool isReady)
    {
      return new Creature(
        creatureId, 1, 0,
        sampleCreatureCard, 0, 0,
        isReady);
    }
  }
}