using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    [Serializable]
    public sealed class ZombieCore
    {
        [field: SerializeField] public LifeComponent LifeComponent { private set; get; }
        [field: SerializeField] public TakeDamageComponent TakeDamageComponent { private set; get; }
        [field: SerializeField] public MoveComponent MoveComponent { private set; get; }
        [field: SerializeField] public LookAtComponent LookAtComponent { private set; get; }

        [field: SerializeField] public AtomicFunction<Vector3> TargetPosition { private set; get; }
        [field: SerializeField] public AtomicValue<float> AttackDistance { private set; get; }
        [field: SerializeField] public AtomicValue<int> AttackDamage { private set; get; }
        [field: SerializeField] public AtomicValue<float> AttackInterval { private set; get; }
        [field: SerializeField] public AtomicVariable<bool> IsInAttackRange { private set; get; }
        [field: SerializeField] public AtomicFunction<bool> IsAttackAvailable { private set; get; }
        [field: SerializeField] public AtomicEvent AttackRequestEvent { private set; get; }
        [field: SerializeField] public AtomicEvent<int> AttackTargetEvent { private set; get; }


        private TakeDamageMechanic _takeDamageMechanic;
        private DirectToPositionMechanic _directToPositionMechanic;
        private MeleeAttackMechanic _meleeAttackMechanic;
        private RangeAttackMechanic _rangeAttackMechanic;

        public void Compose(IAtomicEntity target)
        {
            var targetPosition = target.Get<AtomicVariable<Vector3>>(PlayerApi.POSITION_VARIABLE);
            Func<Vector3> targetPositionFunction = () => targetPosition.Value;
            var takeDamageEvent = target.Get<AtomicEvent<int>>(PlayerApi.TAKE_DAMAGE_EVENT);
            AttackTargetEvent.Subscribe(takeDamageEvent);

            var isDead = target.Get<AtomicVariable<bool>>(PlayerApi.IS_DEAD_COMPONENT);

            TargetPosition.Compose(targetPositionFunction);
            LookAtComponent.Build(targetPositionFunction, () => !LifeComponent.IsDead.Value);
            IsAttackAvailable.Compose(() =>
                !isDead.Value &&
                !LifeComponent.IsDead.Value &&
                IsInAttackRange.Value 
            );
            MoveComponent.Compose(() => !LifeComponent.IsDead.Value && !IsInAttackRange.Value);
        }

        public void Build()
        {
            LifeComponent.Build();
            _takeDamageMechanic = new TakeDamageMechanic(TakeDamageComponent.TakeDamage, LifeComponent.Hp);
            _directToPositionMechanic = new DirectToPositionMechanic(
                TargetPosition,
                MoveComponent.MoveDirection,
                MoveComponent.Position
            );
            _meleeAttackMechanic = new MeleeAttackMechanic(
                targetPosition: TargetPosition,
                currentPosition: MoveComponent.Position,
                attackDistance: AttackDistance,
                attackEvent: AttackRequestEvent,
                attackAvailable: IsAttackAvailable,
                attackInterval: AttackInterval);
            _rangeAttackMechanic = new RangeAttackMechanic(
                attackDistance: AttackDistance,
                position: MoveComponent.Position,
                target: TargetPosition,
                inAttackRange: IsInAttackRange
            );
        }

        public void Enable()
        {
            LifeComponent.Enable();
            _takeDamageMechanic.Enable();
        }

        public void Update()
        {
            _directToPositionMechanic.Update();
            _meleeAttackMechanic.Update();
            _rangeAttackMechanic.Update();
        }

        public void Disable()
        {
            LifeComponent.Disable();
            _takeDamageMechanic.Disable();
        }
    }
}