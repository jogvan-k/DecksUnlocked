using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace KeyforgeUnlocked.Types.HistoricData
{
  public class LazyHistoricData : IMutableHistoricData, IHistoricData
  {
    [NotNull] readonly ImmutableHistoricData _initial;
    IMutableHistoricData _inner;

    public bool ActionPlayedThisTurn
    {
      get => Get().ActionPlayedThisTurn;
      set => Mutable().ActionPlayedThisTurn = value;
    }

    public int EnemiesDestroyedInAFightThisTurn
    {
      get => Get().EnemiesDestroyedInAFightThisTurn;
      set => Mutable().EnemiesDestroyedInAFightThisTurn = value;
    }

    public IImmutableSet<IIdentifiable> CreaturesAttackedThisTurn
    {
      get => Get().CreaturesAttackedThisTurn;
      set => Mutable().CreaturesAttackedThisTurn = value;
    }

    public LazyHistoricData()
    {
      _initial = new ImmutableHistoricData();
    }

    public LazyHistoricData(IHistoricData initial)
    {
      _initial = initial.ToImmutable();
    }

    public ImmutableHistoricData ToImmutable()
    {
      if (_inner != null)
        return _inner.ToImmutable();
      return _initial;
    }

    IHistoricData Get()
    {
      return (IHistoricData) _inner ?? _initial;
    }

    IMutableHistoricData Mutable()
    {
      return _inner ??= new MutableHistoricData(_initial);
    }

    public IMutableHistoricData ToMutable()
    {
      return _inner != null ? _inner.ToMutable() : _initial.ToMutable();
    }
  }
}