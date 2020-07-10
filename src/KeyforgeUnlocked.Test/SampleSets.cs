using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.CreatureCards;
using UnlockedCore.States;

namespace KeyforgeUnlockedTest
{
  public class SampleSets
  {
    static Card[] _sampleDeck = Enumerable.Range(1, 36).Select(i => new SimpleCreatureCard()).ToArray();
    static Card[] _sampleSet = Enumerable.Range(1, 6).Select(i => new SimpleCreatureCard()).ToArray();

    public static Stack<Card> SampleDeck => new Stack<Card>(_sampleDeck);

    public static ISet<Card> SampleSet = new HashSet<Card>(_sampleSet);

    public static Dictionary<Player, ISet<Card>> SampleHands => new Dictionary<Player, ISet<Card>>
    {
      {Player.Player1, SampleSet},
      {Player.Player2, SampleSet}
    };
  }
}