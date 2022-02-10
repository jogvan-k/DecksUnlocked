using System.Text.Json;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Serializers
{
    public class StateSerializer
    {
        public string Serialize(ImmutableState state)
        {
            return JsonSerializer.Serialize(state.ToDto());
        }

        public ImmutableState Deserialize(string str)
        {
            return JsonSerializer.Deserialize<StateDto>(str).ToImmutableState();
        }
    }
}