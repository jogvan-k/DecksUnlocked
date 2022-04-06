using System.Collections.Generic;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
    public class NoActionGroupDto : ActionGroupDto
    {
        public NoActionGroupDto()
        {
            Name = "NoActionGroup";
            Actions = new List<ActionDto>();
        }
    }
}