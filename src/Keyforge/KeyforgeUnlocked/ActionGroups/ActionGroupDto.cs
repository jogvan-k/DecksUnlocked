using System.Collections.Generic;
using KeyforgeUnlocked.Actions;

namespace KeyforgeUnlocked.ActionGroups
{
    public abstract class ActionGroupDto
    {
        public string Name { get; set; }
        public List<ActionDto> Actions { get; set; }
    }
}