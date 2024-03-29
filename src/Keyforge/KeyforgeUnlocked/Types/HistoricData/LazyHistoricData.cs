﻿using System.Collections.Immutable;
using KeyforgeUnlocked.Cards;
using UnlockedCore;

namespace KeyforgeUnlocked.Types.HistoricData
{
    public class LazyHistoricData : IMutableHistoricData, IHistoricData
    {
        readonly ImmutableHistoricData _initial;
        IMutableHistoricData? _inner;

        Lookup<Player, int> IMutableHistoricData.NumberOfShuffles
        {
            get => Mutable().NumberOfShuffles;
            set => Mutable().NumberOfShuffles = value;
        }

        ImmutableLookup<Player, int> IHistoricData.NumberOfShuffles => Get().NumberOfShuffles;

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

        public IImmutableSet<ICard> CardsDiscardedThisTurn
        {
            get => Get().CardsDiscardedThisTurn;
            set => Mutable().CardsDiscardedThisTurn = value;
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
            if (_inner == null) return _initial;
            return (IHistoricData)_inner;
        }

        IMutableHistoricData Mutable()
        {
            return _inner ??= new MutableHistoricData(_initial);
        }

        public IMutableHistoricData ToMutable()
        {
            return _inner != null ? _inner.ToMutable() : _initial.ToMutable();
        }

        public override bool Equals(object? obj)
        {
            return Get().Equals(obj);
        }

        public override int GetHashCode()
        {
            return Get().GetHashCode();
        }
    }
}