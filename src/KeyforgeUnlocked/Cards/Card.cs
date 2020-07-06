using System;
using UnlockedCore.Actions;

namespace KeyforgeUnlocked.Cards
{
  public abstract class Card
  {
    public string Name { get; }

    public House House { get; }

    public CardType CardType { get; }

    protected Card(string name, House house, CardType cardType)
    {
      Name = name;
      House = house;
      CardType = cardType;
    }

    public abstract CoreAction[] Actions(State state);

    public override string ToString()
    {
      return Name;
    }
  }
}