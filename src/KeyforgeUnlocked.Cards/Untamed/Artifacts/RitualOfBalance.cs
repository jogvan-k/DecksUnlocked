using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Artifacts
{
  [CardName("Ritual of Balance")]
  public class RitualOfBalance : ArtifactCard
  {
    static readonly ArtifactType[] types = {ArtifactType.Power};

    static readonly Callback ActionAbility = (s, _) =>
    {
      if(s.Aember[s.playerTurn.Other()] >= 6)
        s.StealAember(s.playerTurn);
    };
    public RitualOfBalance() : this(House.Untamed)
    {
    }
    
    public RitualOfBalance(House house) : base(house, types, actionAbility: ActionAbility)
    {
    }
  }
}