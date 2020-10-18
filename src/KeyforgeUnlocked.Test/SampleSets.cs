using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using UnlockedCore;

namespace KeyforgeUnlockedTest
{
  public class SampleSets
  {
    static readonly Card[] _sampleDeck = Enumerable.Range(1, 36).Select(i => new SampleCreatureCard()).ToArray();
    static readonly Card[] _sampleSet = Enumerable.Range(1, 6).Select(i => new SampleCreatureCard()).ToArray();

    public static Stack<Card> SampleDeck => new Stack<Card>(_sampleDeck);

    public static IMutableSet<Card> SampleSet = new LazySet<Card>(_sampleSet);

    public static IImmutableDictionary<Player, IMutableSet<Card>> SampleHands => new Dictionary<Player, IMutableSet<Card>>
    {
      {Player.Player1, SampleSet},
      {Player.Player2, SampleSet}
    }.ToImmutableDictionary();
  }
}