using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Types;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Types
{
  [TestFixture]
  sealed class EqualityComparerTest_ImmutableSet
  {
    IReadOnlyDictionary<int, IMutableSet<int>> setSut1 ,setSut2;

    [SetUp]
    public void SetUp()
    {
      setSut1 = InitEmptySet();
      setSut2 = InitEmptySet();
    }

    [Test]
    public void ImmutableSet_Empty()
    {
      var imSetSut1 = setSut1.ToImmutable();
      var imSetSut2 = setSut2.ToImmutable();

      Assert.True(EqualityComparer.Equals(imSetSut1, imSetSut2));
      Assert.That(EqualityComparer.GetHashCode(imSetSut1), Is.EqualTo(EqualityComparer.GetHashCode(imSetSut2)));
    }

    [Test]
    public void ImmutableSet_DifferentElements()
    {
      setSut1[1].Add(1);
      setSut2[1].Add(2);
      var imSetSut1 = setSut1.ToImmutable();
      var imSetSUt2 = setSut2.ToImmutable();

      Assert.False(EqualityComparer.Equals(imSetSut1, imSetSUt2));
      Assert.That(EqualityComparer.GetHashCode(imSetSut1), Is.Not.EqualTo(EqualityComparer.GetHashCode(imSetSUt2)));
    }

    [Test]
    public void ImmutableSet_SameElementsInDifferentOrder()
    {
      setSut1[1].Add(1);
      setSut1[1].Add(2);
      
      setSut2[1].Add(2);
      setSut2[1].Add(1);

      var imSetSut1 = setSut1.ToImmutable();
      var imSetSut2 = setSut2.ToImmutable();
      
      Assert.True(EqualityComparer.Equals(imSetSut1, imSetSut2));
      Assert.That(EqualityComparer.GetHashCode(imSetSut1), Is.EqualTo(EqualityComparer.GetHashCode(imSetSut2)));
    }

    static Dictionary<int, IMutableSet<int>> InitEmptySet()
    {
      return new Dictionary<int, IMutableSet<int>>
        {{1, new LazySet<int>()}};
    }
  }
}