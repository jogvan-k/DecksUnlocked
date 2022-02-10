using System.Collections.Generic;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.States
{
    public class StateDto
    {
        public Player PlayerTurn { get; set; }
        public int TurnNumber { get; set; }
        public bool IsGameOver { get; set; }
        public House? ActiveHouse { get; set; }
        public Dictionary<Player, int> Keys { get; set; }

        public Dictionary<Player, int> Aember { get; set; }

        //public IImmutableSet<IActionGroup> ActionGroups { get; set; }
        public Dictionary<Player, List<CardDto>> Decks { get; set; }
        public Dictionary<Player, List<CardDto>> Hands { get; set; }
        public Dictionary<Player, List<CardDto>> Discards { get; set; }
        public Dictionary<Player, List<CardDto>> Archives { get; set; }

        public Dictionary<Player, List<CardDto>> PurgedCard { get; set; }

        //public Dictionary<Player, List<CreatureDTO>> Fields { get; set; }
        public Dictionary<Player, List<ArtifactDto>> Artifacts { get; set; }
        //public ImmutableArray<IEffect> Effects { get; set; }
        //public ImmutableEvents Events { get; set; }
        public List<IResolvedEffect> ResolvedEffects { get; set; }

        //public ImmutableHistoricData HistoricData { get; set; }
        public MetadataDto Metadata { get; set; }
    }
}