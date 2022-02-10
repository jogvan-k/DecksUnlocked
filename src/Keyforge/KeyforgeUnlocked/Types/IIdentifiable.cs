using System;

namespace KeyforgeUnlocked.Types
{
    public interface IIdentifiable : IComparable
    {
        string Id { get; }

        string Name { get; }

        public bool Equals(IIdentifiable other)
        {
            return Id.Equals(other.Id);
        }

        public int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}