using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards
{
    public abstract class ActionCard : Card, IActionCard
    {
        protected ActionCard(
            House house,
            Pip[]? pips = null,
            Callback? playAbility = null) : base(house, pips: pips, playAbility: playAbility)
        {
        }
    }
}