using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Actions;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.ActionGroups
{
  [TestFixture]
  public class EndTurnGroupTest
  {
    [Test]
    public void Actions()
    {
      var sut = new EndTurnGroup();

      var actions = sut.Actions;

      Assert.AreEqual(new EndTurn(), actions.Single());
    }
  }
}