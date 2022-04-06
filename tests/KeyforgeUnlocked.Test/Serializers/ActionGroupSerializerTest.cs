using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Serializers;
using KeyforgeUnlocked.Types;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Serializers
{
    [TestFixture]
    public class ActionGroupSerializerTest
    {
        [Test]
        public void EndTurnActionGroup_RoundtripTest()
        {
            var actionGroup = new EndTurnGroup();

            var result = actionGroup.ToDto().ToActionGroup();

            Assert.That(result, Is.EqualTo(actionGroup));
        }
    }
}