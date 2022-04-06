using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
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
        }

        [Test]
        public void NoActionGroup_RoundtripTest()
        {
            var actionGroup = new NoActionGroup();

            var actionGroupDto = actionGroup.ToDto();
            var result = actionGroupDto.ToActionGroup();

            Assert.That(result, Is.EqualTo(actionGroup));
            Assert.That(actionGroupDto.Name, Is.EqualTo("NoActionGroup"));
        }

        [Test]
        public void DeclareHouseGroup_RoudtripTest()
        {
            var actionGroup = new DeclareHouseGroup(new[] { House.Brobnar, House.Dis });

            var actionGroupDto = actionGroup.ToDto();
            var result = actionGroupDto.ToActionGroup();

            Assert.That(result, Is.EqualTo(actionGroup));
            Assert.That(actionGroupDto.Name, Is.EqualTo("DeclareHouseGroup"));
            Assert.That(((DeclareHouseGroup) result).Houses, Is.EqualTo(actionGroup.Houses));
        }
    }
}