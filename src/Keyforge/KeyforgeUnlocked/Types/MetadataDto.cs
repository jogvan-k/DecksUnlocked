using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using UnlockedCore;

namespace KeyforgeUnlocked.Types
{
    public class MetadataDto
    {
        public Dictionary<Player, List<CardDto>> InitialDecks { get; set; }
        public Dictionary<Player, List<House>> Houses { get; set; }
        public int TurnCountLimit { get; set; }
        public int RngSeed { get; set; }
    }
}