namespace KeyforgeUnlocked.Types.Events
{
  public interface IMutableEvents : IEvents
  {
    void Subscribe(IIdentifiable source, EventType type, Callback callback);
    void Subscribe(IIdentifiable source, ModifierType type, Modifier modifier);
    void Unsubscribe(string id);
  }
}