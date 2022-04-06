using System;

namespace KeyforgeUnlocked.ActionGroups
{
    public static class ActionGroupExtensions
    {
        public static ActionGroupDto ToDto(this IActionGroup actionGroup) => actionGroup.GetType().Name switch
        {
            "EndTurnGroup" => new EndTurnDto(),
            { } name => throw new ArgumentOutOfRangeException(name)
        };

        public static IActionGroup ToActionGroup(this ActionGroupDto dto) => dto.Name switch
        {
            "EndTurnGroup" => new EndTurnGroup(),
            { } name => throw new ArgumentOutOfRangeException(name)
        };
    }
}