using System;
using System.Collections.Generic;
using System.Linq;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Effects;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;
using KeyforgeUnlocked.Types.HistoricData;
using KeyforgeUnlockedTest.Util;
using NUnit.Framework;

namespace KeyforgeUnlockedTest.Effects.FightCreatureTests
{
    abstract class FightCreatureTestBase
    {
        protected const string FightingCreatureId = "F";
        protected const string TargetCreatureId = "T";

        protected IIdentifiable _fightingCreature = new Identifiable(FightingCreatureId);
        protected IIdentifiable _targetCreature = new Identifiable(TargetCreatureId);
        protected bool _fightingCreatureBeforeFightAbilityResolved;
        protected bool _fightingCreatureFightAbilityResolved;
        protected bool _fightingCreatureDestroyedAbilityResolved;
        protected bool _fightingCreatureAfterKillAbilityResolved;
        protected bool _targetCreatureBeforeFightAbilityResolved;
        protected bool _targetCreatureFightAbilityResolved;
        protected bool _targetCreatureDestroyedAbilityResolved;
        protected bool _targetCreatureAfterKillAbilityResolved;

        protected bool _fightingCreatureDestroyedEventInvoked;
        protected bool _targetCreatureDestroyedEventInvoked;

        protected Callback _fightingCreatureBeforeFightAbility;
        protected Callback _fightingCreatureFightAbility;
        protected Callback _fightingCreatureDestroyedAbility;
        protected Callback _fightingCreatureAfterKillAbility;
        protected Callback _targetCreatureBeforeFightAbility;
        protected Callback _targetCreatureFightAbility;
        protected Callback _targetCreatureDestroyedAbility;
        protected Callback _targetCreatureAfterKillAbility;

        protected Callback _creatureDestroyedEvent;

        [SetUp]
        public void SetUp()
        {
            _fightingCreatureBeforeFightAbilityResolved = false;
            _fightingCreatureDestroyedAbilityResolved = false;
            _fightingCreatureFightAbilityResolved = false;
            _fightingCreatureDestroyedAbilityResolved = false;
            _fightingCreatureAfterKillAbilityResolved = false;
            _targetCreatureBeforeFightAbilityResolved = false;
            _targetCreatureFightAbilityResolved = false;
            _targetCreatureDestroyedAbilityResolved = false;
            _targetCreatureAfterKillAbilityResolved = false;
            _fightingCreatureBeforeFightAbility = (_, _, _) => _fightingCreatureBeforeFightAbilityResolved = true;
            _fightingCreatureFightAbility = (_, _, _) => _fightingCreatureFightAbilityResolved = true;
            _fightingCreatureDestroyedAbility = (_, _, _) => _fightingCreatureDestroyedAbilityResolved = true;
            _fightingCreatureAfterKillAbility = (_, _, _) => _fightingCreatureAfterKillAbilityResolved = true;
            _targetCreatureBeforeFightAbility = (_, _, _) => _targetCreatureBeforeFightAbilityResolved = true;
            _targetCreatureFightAbility = (_, _, _) => _targetCreatureFightAbilityResolved = true;
            _targetCreatureDestroyedAbility = (_, _, _) => _targetCreatureDestroyedAbilityResolved = true;
            _targetCreatureAfterKillAbility = (_, _, _) => _targetCreatureAfterKillAbilityResolved = true;

            _fightingCreatureDestroyedEventInvoked = false;
            _targetCreatureDestroyedEventInvoked = false;
            _creatureDestroyedEvent = (_, id, _) =>
            {
                if (id.Equals(_fightingCreature))
                    _fightingCreatureDestroyedEventInvoked = true;
                else if (id.Equals(_targetCreature))
                    _targetCreatureDestroyedEventInvoked = true;
                else
                    throw new Exception($"Unknown target {id}");
            };
        }

        protected IMutableState SetupAndAct(
            SampleCreatureCard fightingCreatureCard,
            SampleCreatureCard targetCreatureCard,
            int fightingCreatureBrokenArmor = 0,
            int targetCreatureBrokenArmor = 0)
        {
            var fightingCreature = new Creature(fightingCreatureCard, isReady: true,
                brokenArmor: fightingCreatureBrokenArmor);
            var targetCreature =
                new Creature(targetCreatureCard, isReady: true, brokenArmor: targetCreatureBrokenArmor);
            var fields = TestUtil.Lists(fightingCreature, targetCreature);
            var events = Events();
            var state = StateTestUtil.EmptyState.New(fields: fields, events: events);
            var sut = new FightCreature(fightingCreature, targetCreature);

            sut.Resolve(state);

            return state;
        }

        IMutableEvents Events()
        {
            var events = new LazyEvents();
            events.Subscribe(new Identifiable(""), EventType.CreatureDestroyed, _creatureDestroyedEvent);
            return events;
        }

        protected IMutableState ExpectedState(
            Creature expectedFighter,
            Creature expectedTarget,
            bool fightOccured = true)
        {
            var fighterDead = expectedFighter.Health <= 0;
            var targetDead = expectedTarget.Health <= 0;
            var expectedFields = TestUtil.Lists(
                fighterDead ? Enumerable.Empty<Creature>() : new[] { expectedFighter },
                targetDead ? Enumerable.Empty<Creature>() : new[] { expectedTarget });

            var expectedDiscards = TestUtil.Sets(
                fighterDead ? new[] { expectedFighter.Card } : Enumerable.Empty<ICard>(),
                targetDead ? new[] { expectedTarget.Card } : Enumerable.Empty<ICard>());

            var resolvedEffects = new List<IResolvedEffect>();

            if (fightOccured) resolvedEffects.Add(new CreatureFought(expectedFighter, expectedTarget));
            if (fighterDead)
            {
                resolvedEffects.Add(new CreatureDied(expectedFighter));
            }

            if (targetDead)
            {
                resolvedEffects.Add(new CreatureDied(expectedTarget));
            }

            var events = Events();
            var historicData = new LazyHistoricData();

            if (fightOccured)
            {
                historicData.CreaturesAttackedThisTurn =
                    historicData.CreaturesAttackedThisTurn.Add(new Identifiable(expectedTarget));
                if (targetDead)
                    historicData.EnemiesDestroyedInAFightThisTurn += 1;
            }

            return StateTestUtil.EmptyState.New(
                fields: expectedFields, discards: expectedDiscards,
                resolvedEffects: new LazyList<IResolvedEffect>(resolvedEffects), events: events,
                historicData: historicData);
        }

        protected void Assert(IState expectedState, IState actualState, bool expectedFighterDead,
            bool expectedTargetDead, bool fightOccured = true)
        {
            NUnit.Framework.Assert.True(_fightingCreatureBeforeFightAbilityResolved);
            NUnit.Framework.Assert.False(_targetCreatureBeforeFightAbilityResolved);
            NUnit.Framework.Assert.AreEqual(!expectedFighterDead && fightOccured,
                _fightingCreatureFightAbilityResolved);
            NUnit.Framework.Assert.False(_targetCreatureFightAbilityResolved);
            NUnit.Framework.Assert.AreEqual(expectedFighterDead, _fightingCreatureDestroyedAbilityResolved);
            NUnit.Framework.Assert.AreEqual(expectedTargetDead, _targetCreatureDestroyedAbilityResolved);
            NUnit.Framework.Assert.AreEqual(!expectedFighterDead && expectedTargetDead && fightOccured,
                _fightingCreatureAfterKillAbilityResolved);
            NUnit.Framework.Assert.AreEqual(expectedFighterDead && !expectedTargetDead && fightOccured,
                _targetCreatureAfterKillAbilityResolved);

            NUnit.Framework.Assert.AreEqual(_fightingCreatureDestroyedEventInvoked, expectedFighterDead);
            NUnit.Framework.Assert.AreEqual(_targetCreatureDestroyedEventInvoked, expectedTargetDead);
            StateAsserter.StateEquals(expectedState, actualState);
        }

        protected SampleCreatureCard InstantiateFightingCreatureCard(int power, int armor = 0,
            Keyword[] keywords = null)
        {
            return new(power: power, armor: armor,
                beforeFightAbility: _fightingCreatureBeforeFightAbility,
                fightAbility: _fightingCreatureFightAbility,
                afterKillAbility: _fightingCreatureAfterKillAbility,
                destroyedAbility: _fightingCreatureDestroyedAbility,
                keywords: keywords,
                id: FightingCreatureId);
        }

        protected SampleCreatureCard InstantiateTargetCreatureCard(int power, int armor = 0, Keyword[] keywords = null)
        {
            return new(power: power, armor: armor, beforeFightAbility: _targetCreatureBeforeFightAbility,
                fightAbility: _targetCreatureFightAbility,
                afterKillAbility: _targetCreatureAfterKillAbility, destroyedAbility: _targetCreatureDestroyedAbility,
                keywords: keywords,
                id: TargetCreatureId);
        }
    }
}