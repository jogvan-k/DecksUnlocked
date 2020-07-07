using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Creatures;
using UnlockedCore.Actions;

namespace KeyforgeUnlocked.Cards
{
  public abstract class CreatureCard : Card
  {
    public int Power { get; }
    public int Armor { get; }

    protected CreatureCard(
      string name,
      House house,
      int power,
      int armor) : base(name, house, CardType.Creature)
    {
      Power = power;
      Armor = armor;
    }

    public override CoreAction[] Actions(State state)
    {
      var boardLength = state.Fields[state.PlayerTurn].Count;

      var leftPosition = new PlayCreature(state, this, 0);
      if (boardLength > 0)
      {
        return new CoreAction[] {leftPosition, new PlayCreature(state, this, boardLength)};
      }

      return new CoreAction[] {leftPosition};
    }

    public Creature InsantiateCreature()
    {
      return new Creature(Power, Armor, this);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((CreatureCard) obj);
    }
  }
}