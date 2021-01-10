using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Algorithms;
using KeyforgeUnlocked.Types;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Algorithms
{
  [TestFixture]
  sealed class ShufflerTest
  {
    static IReadOnlyList<IIdentifiable> _initialCards = Enumerable.Range(0, 10)
      .Select(i => (IIdentifiable) new SampleCreatureCard(id: i.ToString()))
      .ToList();

    [Test]
    public void Shuffle_SameSeedProducesSameResult()
    {
      var firstShuffle = Shuffler.Shuffle(_initialCards, 0);
      var secondShuffle = Shuffler.Shuffle(_initialCards, 0);

      Assert.True(EqualityComparer.Equals(firstShuffle, secondShuffle));
      Assert.False(EqualityComparer.Equals(_initialCards, firstShuffle));
    }

    [Test]
    public void Shuffle_DifferentSeedProducesDifferentResult()
    {
      var firstShuffle = Shuffler.Shuffle(_initialCards, 1);
      var secondShuffle = Shuffler.Shuffle(_initialCards, 2);
      
      Assert.False(EqualityComparer.Equals(firstShuffle, secondShuffle));
      Assert.False(EqualityComparer.Equals(_initialCards, firstShuffle));
      Assert.False(EqualityComparer.Equals(_initialCards, secondShuffle));
    }

    [Test]
    public void ShuffleWithSuperSequence()
    {
      var subset = _initialCards.Skip(3).Take(5);
      var supersetShuffle = Shuffler.Shuffle(_initialCards, 0);
      var shuffle = Shuffler.Shuffle(_initialCards, subset, 0);

      Assert.That(shuffle.Count, Is.EqualTo(subset.Count()));
      var i = -1;
      foreach (var id in shuffle)
      {
        var index = supersetShuffle.IndexOf(id);
        Assert.That(index, Is.GreaterThan(i));
        i = index;
      }
    }

    [Test]
    public void ShuffleWithSuperSequence_SubjectContainsCardNotInSuperset_throwException()
    {
      var subset = _initialCards.Skip(3).Take(4).Concat(new[] {new SampleCreatureCard(id: "invalid")});
      try
      {
        Shuffler.Shuffle(_initialCards, subset, 0);
      }
      catch (Exception e)
      {
        return;
      }
      Assert.Fail();
    }
  }
}