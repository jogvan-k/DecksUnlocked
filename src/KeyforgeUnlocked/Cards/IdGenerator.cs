namespace KeyforgeUnlocked.Cards
{
  public static class IdGenerator
  {
    static int nextId;

    public static int GetNextInt()
    {
      return nextId++;
    }
  }
}