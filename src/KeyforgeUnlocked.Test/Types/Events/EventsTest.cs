using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using Moq;
using NUnit.Framework;
using UnlockedCore;

namespace KeyforgeUnlockedTest.Types.Events
{
  [TestFixture]
  sealed class EventsTest
  {
    readonly IIdentifiable Source1 = new Identifiable("id1");
    readonly IIdentifiable Source2 = new Identifiable("id2");
    
    [Theory]
    public void SubscribeRaiseEventUnsubscribe(EventType type)
    {
      var fun1Invoked = 0;
      var fun2Invoked = 0;
      Callback fun1 = (_, _, _) => { fun1Invoked += 1;};
      Callback fun2 = (_, _, _) => { fun2Invoked += 1;};

      var sut = new KeyforgeUnlocked.Types.Events.Events();
      sut.RaiseEvent(type, null, null, Player.None);

      Assert.That(fun1Invoked, Is.EqualTo(0));
      Assert.That(fun2Invoked, Is.EqualTo(0));
      
      sut.Subscribe(Source1, type, fun1);
      sut.Subscribe(Source2, type, fun2);
      sut.RaiseEvent(type, null, null, Player.None);
      
      Assert.That(fun1Invoked, Is.EqualTo(1));
      Assert.That(fun2Invoked, Is.EqualTo(1));
      
      sut.Unsubscribe(Source1.Id, type);
      sut.RaiseEvent(type, null, null, Player.None);
      
      Assert.That(fun1Invoked, Is.EqualTo(1));
      Assert.That(fun2Invoked, Is.EqualTo(2));
      
      sut.Unsubscribe(Source2.Id, type);
      sut.RaiseEvent(type, null, null, Player.None);
      
      Assert.That(fun1Invoked, Is.EqualTo(1));
      Assert.That(fun2Invoked, Is.EqualTo(2));
    }

    [Test]
    public void SubscribeUntilEndOfTurn()
    {
      var funInvoked = 0;
      Callback fun = (_, _, _) => funInvoked++;
      var state = SetupState();

      state.Events.SubscribeUntilEndOfTurn(Source1, EventType.CreatureDestroyed, fun);
      state.RaiseEvent(EventType.CreatureDestroyed);
      state.RaiseEvent(EventType.CreatureDestroyed);
      state.RaiseEvent(EventType.TurnEnded);
      state.RaiseEvent(EventType.CreatureDestroyed);

      Assert.That(funInvoked, Is.EqualTo(2));
    }

    [Test]
    public void SubscribeUntilLeavesPlay(
      [Values(EventType.CreatureDestroyed, EventType.CreatureReturnedToHand)] EventType destructorEvent)
    {
      var funInvoked = 0;
      Callback fun = (_, _, _) => funInvoked++;

      var sourceId = new Identifiable("source");
      var otherId = new Identifiable("other");
      var state = SetupState();
      var sut = state.Events;
      
      sut.SubscribeUntilLeavesPlay(sourceId, EventType.KeyForged, fun);
      
      sut.RaiseEvent(EventType.KeyForged, null, null, Player.None);
      sut.RaiseEvent(destructorEvent, state, otherId, Player.None);
      sut.RaiseEvent(EventType.KeyForged, null, null, Player.None);
      sut.RaiseEvent(destructorEvent, state, sourceId, Player.None);
      sut.RaiseEvent(EventType.KeyForged, null, null, Player.None);
      
      Assert.That(funInvoked, Is.EqualTo(2));
    }

    static IMutableState SetupState()
    {
      var stateMock = new Mock<IMutableState>(MockBehavior.Strict);
      stateMock.SetupProperty(s => s.Events);
      var state = stateMock.Object;
      state.Events = new KeyforgeUnlocked.Types.Events.Events();
      return state;
    }
  }
}