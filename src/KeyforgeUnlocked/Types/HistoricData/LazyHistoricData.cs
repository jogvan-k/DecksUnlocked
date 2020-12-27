using System.Diagnostics.CodeAnalysis;

namespace KeyforgeUnlocked.Types.HistoricData
{
  public class LazyHistoricData : IMutableHistoricData
  {
    [NotNull] readonly IImmutableHistoricData _initial;
    IMutableHistoricData _inner;

    public bool ActionPlayedThisTurn
    {
      get => Get().ActionPlayedThisTurn;
      set => Mutable().ActionPlayedThisTurn = value;
    }

    public LazyHistoricData(IHistoricData initial)
    {
      _initial = initial.ToImmutable();
    }

    public LazyHistoricData(IImmutableHistoricData initial)
    {
      _initial = initial;
    }

    public IImmutableHistoricData ToImmutable()
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