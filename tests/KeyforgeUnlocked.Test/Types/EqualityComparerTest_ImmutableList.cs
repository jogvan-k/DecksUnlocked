using System.Collections.Generic;
using KeyforgeUnlocked.Types;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Types
{
    public class EqualityComparerTest_ImmutableList
    {
        IReadOnlyDictionary<int, IMutableList<int>> listSut1, listSut2;

        [SetUp]
        public void SetUp()
        {
            listSut1 = InitEmptyList();
            listSut2 = InitEmptyList();
        }

        [Test]
        public void ImmutableList_Empty()
        {
            var imListSut1 = listSut1.ToImmutable();
            var imListSut2 = listSut2.ToImmutable();

            Assert.True(EqualityComparer.Equals(imListSut1, imListSut2));
            Assert.That(EqualityComparer.GetHashCode(imListSut1), Is.EqualTo(EqualityComparer.GetHashCode(imListSut2)));
        }

        [Test]
        public void ImmutableSet_DifferentElements()
        {
            listSut1[1].Add(1);
            listSut2[1].Add(2);
            var imListSut1 = listSut1.ToImmutable();
            var imListSUt2 = listSut2.ToImmutable();

            Assert.False(EqualityComparer.Equals(imListSut1, imListSUt2));
            Assert.That(EqualityComparer.GetHashCode(imListSut1),
                Is.Not.EqualTo(EqualityComparer.GetHashCode(imListSUt2)));
        }

        [Test]
        public void ImmutableSet_SameElementsInDifferentOrder()
        {
            listSut1[1].Add(1);
            listSut1[1].Add(2);

            listSut2[1].Add(2);
            listSut2[1].Add(1);

            var imSetSut1 = listSut1.ToImmutable();
            var imSetSut2 = listSut2.ToImmutable();

            Assert.False(EqualityComparer.Equals(imSetSut1, imSetSut2));
            Assert.That(EqualityComparer.GetHashCode(imSetSut1),
                Is.Not.EqualTo(EqualityComparer.GetHashCode(imSetSut2)));
        }

        static Dictionary<int, IMutableList<int>> InitEmptyList()
        {
            return new Dictionary<int, IMutableList<int>>
                { { 1, new LazyList<int>() } };
        }
    }
}