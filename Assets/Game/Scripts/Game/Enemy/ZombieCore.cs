using System;
using Atomic.Elements;
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


        private TakeDamageMechanic _takeDamageMechanic;
        private DirectToPositionMechanic _directToPositionMechanic;

        public void Compose(Func<Vector3> targetPosition)
        {
            TargetPosition.Compose(targetPosition);
            LookAtComponent.Compose(targetPosition);
        }
        
        public void Build()
        {
            _takeDamageMechanic = new TakeDamageMechanic(TakeDamageComponent.TakeDamage, LifeComponent.Hp);
            _directToPositionMechanic = new DirectToPositionMechanic(
                TargetPosition,
                MoveComponent.MoveDirection,
                MoveComponent.Position
            );
        }

        public void Enable()
        {
            _takeDamageMechanic.Enable();
        }

        public void Update()
        {
            _directToPositionMechanic.Update();
        }

        public void Disable()
        {
            _takeDamageMechanic.Disable();
        }
    }
}