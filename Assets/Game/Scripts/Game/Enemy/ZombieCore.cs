using System;
using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public sealed class ZombieCore : MonoBehaviour
    {
        [field: SerializeField] public LifeComponent LifeComponent { private set; get; }
        [field: SerializeField] public TakeDamageComponent TakeDamageComponent { private set; get; }
        [field: SerializeField] public MoveComponent MoveComponent { private set; get; }
        [field: SerializeField] public LookAtComponent LookAtComponent { private set; get; }

        [field: SerializeField] public AtomicFunction<Vector3> TargetPosition { private set; get; }

        private TakeDamageMechanic _takeDamageMechanic;
        private DirectToPositionMechanic _directToPositionMechanic;

        public void Build(Func<Vector3> targetPosition)
        {
            TargetPosition.Compose(targetPosition);
        }

        private void Awake()
        {
            _takeDamageMechanic = new TakeDamageMechanic(TakeDamageComponent.TakeDamage, LifeComponent.Hp);
            _directToPositionMechanic = new DirectToPositionMechanic(
                TargetPosition,
                MoveComponent.MoveDirection,
                MoveComponent.Position
            );
        }

        private void OnEnable()
        {
            _takeDamageMechanic.Enable();
        }

        private void Update()
        {
            _directToPositionMechanic.Update();
            
            // todo remove to mechanic
            LookAtComponent.LookAtPoint.Value = TargetPosition.Invoke();
        }

        private void OnDisable()
        {
            _takeDamageMechanic.Disable();
        }
    }
}