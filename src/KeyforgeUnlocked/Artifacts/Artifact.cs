using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Artifacts
{
  public struct Artifact : IIdentifiable
  {
    public readonly IArtifactCard Card;
    public bool IsReady;
    public string Id => Card.Id;
    public string Name => Card.Name;
    public Callback ActionAbility => Card.CardActionAbility;

    public Artifact(IArtifactCard card, bool isReady = false)
    {
      Card = card;
      IsReady = isReady;
    }

    public override bool Equals(object? obj)
    {
      return obj is Artifact other && Equals(other);
    }

    bool Equals(Artifact other)
    {
      return Id.Equals(other.Id) && IsReady.Equals(other.IsReady);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Id, IsReady);
    }

    public override string ToString()
    {
      return
        $"{Card.GetType().Name}, IsReady: {IsReady}";
    }
  }
}