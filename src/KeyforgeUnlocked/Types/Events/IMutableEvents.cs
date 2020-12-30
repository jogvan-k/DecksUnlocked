namespace KeyforgeUnlocked.Types.Events
{
  public interface IMutableEvents : IEvents
  {
    void Subscribe(IIdentifiable source, EventType type, Callback callback);
    void Unsubscribe(string id, EventType type);
  }
}