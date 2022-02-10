using System;
using System.Collections.Generic;
using System.Linq;

namespace KeyforgeUnlocked.Algorithms
{
    public static class Shuffler
    {
        public static IList<T> Shuffle<T>(
            IReadOnlyList<T> superSequence,
            IEnumerable<T> subject,
            int seed)
        {
            var enumerable = subject as T[] ?? subject.ToArray();
            if (enumerable.Except(superSequence).Any())
                throw new Exception("All entries in subject are not contained in super sequence");
            var superShuffle = Shuffle(superSequence, seed);
            return superShuffle.Where(enumerable.Contains).ToList();
        }

        public static IList<T> Shuffle<T>(
            IEnumerable<T> subject,
            int seed)
        {
            var rng = new Random(seed);
            var idAndOrder = new List<(T, int)>();
            foreach (var id in subject)
            {
                idAndOrder.Add((id, rng.Next()));
            }

            return idAndOrder.OrderBy(c => c.Item2).Select(c => c.Item1).ToList();
        }
    }
}