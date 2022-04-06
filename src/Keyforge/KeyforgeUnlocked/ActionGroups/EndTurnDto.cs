using System.Collections.Generic;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
    public class EndTurnDto : ActionGroupDto
    {
        public EndTurnDto()
        {
            Name = "EndTurnGroup";
            Actions = new List<ActionDto>();
        }
    }
}