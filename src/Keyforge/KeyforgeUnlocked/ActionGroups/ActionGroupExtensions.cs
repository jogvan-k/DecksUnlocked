using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ActionGroups
{
    public static class ActionGroupExtensions
    {
        public static ActionGroupDto ToDto(this IActionGroup actionGroup) => actionGroup.GetType().Name switch
        {
            "DeclareHouseGroup" => new ActionGroupDto{ Name = "DeclareHouseGroup", Parameters = JsonSerializer.Serialize(((DeclareHouseGroup) actionGroup).Houses.ToList())},
            { } name => new ActionGroupDto { Name = name }
        };

        public static IActionGroup ToActionGroup(this ActionGroupDto dto) => dto.Name switch
        {
            "EndTurnGroup" => new EndTurnGroup(),
            "NoActionGroup" => new NoActionGroup(),
            "DeclareHouseGroup" => new DeclareHouseGroup(JsonSerializer.Deserialize<List<House>>(dto.Parameters).ToImmutableHashSet()),
            { } name => throw new ArgumentOutOfRangeException(name)
        };
    }
}