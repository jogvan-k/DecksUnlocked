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

            var actionGroupDto = actionGroup.ToDto();
            var result = actionGroupDto.ToActionGroup();

            Assert.That(result, Is.EqualTo(actionGroup));
            Assert.That(actionGroupDto.Name, Is.EqualTo("EndTurnGroup"));
            Assert.That(actionGroupDto.Actions, Is.Empty);
        }

        [Test]
        public void NoActionGroup_RoundtripTest()
        {
            var actionGroup = new NoActionGroup();

            var actionGroupDto = actionGroup.ToDto();
            var result = actionGroupDto.ToActionGroup();

            Assert.That(result, Is.EqualTo(actionGroup));
            Assert.That(actionGroupDto.Name, Is.EqualTo("NoActionGroup"));
            Assert.That(actionGroupDto.Actions, Is.Empty);
        }
    }
}