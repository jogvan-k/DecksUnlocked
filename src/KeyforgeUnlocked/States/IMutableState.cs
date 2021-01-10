using System.Collections.Generic;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Artifacts;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlocked.Types.HistoricData;
using UnlockedCore;

namespace KeyforgeUnlocked.States
{
  public interface IMutableState : IState
  {
    new Player PlayerTurn { get; set; }
    new int TurnNumber { get; set; }
    new bool IsGameOver { get; set; }
    new House? ActiveHouse { get; set; }
    new IMutableLookup<Player, int> Keys { get; set; }
    new IMutableLookup<Player, int> Aember { get; set; }
    new IMutableList<IActionGroup> ActionGroups { get; set; }
    new IReadOnlyDictionary<Player, IMutableStackQueue<ICard>> Decks { get; set; }
    new IReadOnlyDictionary<Player, IMutableSet<ICard>> Hands { get; set; }
    new IReadOnlyDictionary<Player, IMutableSet<ICard>> Discards { get; set; }
    new IReadOnlyDictionary<Player, IMutableSet<ICard>> Archives { get; set; }
    new IReadOnlyDictionary<Player, IMutableSet<ICard>> PurgedCard { get; }
    new IReadOnlyDictionary<Player, IMutableList<Creature>> Fields { get; set; }
    new IReadOnlyDictionary<Player, IMutableSet<Artifact>> Artifacts { get; set; }
    new IMutableStackQueue<IEffect> Effects { get; set; }
    new IMutableEvents Events { get; set; }
    new IMutableList<IResolvedEffect> ResolvedEffects { get; set; }
    new IMutableHistoricData HistoricData { get; set; }
    new Metadata Metadata { get; set; }

    ImmutableState ResolveEffects();
  }
}