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
        [field: SerializeField] public AtomicEvent<int> AttackTargetEvent { private set; get; }
        [field: SerializeField] public AtomicValue<float> AttackDistance { private set; get; }
        [field: SerializeField] public AtomicValue<int> AttackDamage { private set; get; }
        [field: SerializeField] public AtomicValue<float> AttackInterval { private set; get; }
        [field: SerializeField] public AtomicVariable<bool> IsAttacking { private set; get; }
        [field: SerializeField] public AtomicEvent AttackRequestEvent { private set; get; }


        private TakeDamageMechanic _takeDamageMechanic;
        private DirectToPositionMechanic _directToPositionMechanic;
        private MeleeAttackMechanic _meleeAttackMechanic;
        private DamageDealerMechanic _damageDealerMechanic;
        private StopToAttackMechanic _stopToAttackMechanic;

        public void Compose(IAtomicEntity target)
        {
            var targetPosition = target.Get<AtomicVariable<Vector3>>(PlayerApi.POSITION_VARIABLE);
            Func<Vector3> targetPositionFunction = () => targetPosition.Value;
            TargetPosition.Compose(targetPositionFunction);
            LookAtComponent.Compose(targetPositionFunction, () => !LifeComponent.IsDead.Value);

            var takeDamageEvent = target.Get<AtomicEvent<int>>(PlayerApi.TAKE_DAMAGE_EVENT);
            AttackTargetEvent.Subscribe(takeDamageEvent);

            MoveComponent.Compose(() => !LifeComponent.IsDead.Value && !IsAttacking.Value);
        }

        public void Build()
        {
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
                attackEvent: AttackRequestEvent);
            _damageDealerMechanic = new DamageDealerMechanic(
                attackRequestEvent: AttackRequestEvent,
                takeDamageEvent: AttackTargetEvent,
                damage: AttackDamage,
                interval: AttackInterval
            );
            _stopToAttackMechanic = new StopToAttackMechanic(
                attackDistance: AttackDistance,
                position: MoveComponent.Position,
                target: TargetPosition,
                isAttacking: IsAttacking
            );
        }

        public void Enable()
        {
            _takeDamageMechanic.Enable();
            _damageDealerMechanic.Enable();
        }

        public void Update()
        {
            _directToPositionMechanic.Update();
            _meleeAttackMechanic.Update();
            _stopToAttackMechanic.Update();
        }

        public void Disable()
        {
            _takeDamageMechanic.Disable();
            _damageDealerMechanic.Disable();
        }
    }
}