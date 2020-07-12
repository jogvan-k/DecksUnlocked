using System.Collections.Generic;

namespace KeyforgeUnlockedConsole.PrintCommands
{
  public static class PrintCommandsFactory
  {
    public static IDictionary<string, IPrintCommand> HelperCommands => new Dictionary<string, IPrintCommand>
    {
      {"deck", new PrintDeck()},
      {"arc", new PrintArchive()},
      {"dis", new PrintDiscard()}
    };
  }
}