using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Game;
using Game.Scripts.Game.Enemy;
using UnityEngine;
using Cursor = Game.Scripts.Input.Cursor;

namespace Game.Scripts
{
    [Serializable]
    public sealed class PlayerCore
    {
        [field: SerializeField] public MoveComponent MoveComponent { private set; get; }
        [field: SerializeField] public ShootComponent ShootComponent { private set; get; }
        [field: SerializeField] public LookAtComponent LookAtComponent { private set; get; }
        [field: SerializeField] public TakeDamageComponent TakeDamageComponent { private set; get; }
        [field: SerializeField] public LifeComponent LifeComponent { private set; get; }
        [field: SerializeField] public AtomicEvent ShootRequest { private set; get; }
        [field: SerializeField] public AtomicVariable<int> BulletsCount { private set; get; }
        [field: SerializeField] public AtomicValue<int> MaxBullet { private set; get; }
        [field: SerializeField] public AtomicValue<float> RestoreInterval { private set; get; }

        private TakeDamageMechanic _takeDamageMechanic;
        private ShootMechanic _shootMechanic;


        public void Build(Cursor cursor, AtomicObject atomicObject)
        {
            LifeComponent.Build(atomicObject);
            LookAtComponent.Build(() => cursor.Position, () => !LifeComponent.IsDead.Value, atomicObject);
            var isShootingAvailable = new AtomicFunction<bool>(() => !LifeComponent.IsDead.Value);
            MoveComponent.Compose(() => !LifeComponent.IsDead.Value, atomicObject);
            _takeDamageMechanic = new TakeDamageMechanic(TakeDamageComponent.TakeDamage, LifeComponent.Hp);
            _shootMechanic = new ShootMechanic(
                BulletsCount,
                MaxBullet,
                RestoreInterval,
                ShootRequest,
                isShootingAvailable,
                ShootComponent.ShootEvent
            );
        }

        public void Enable()
        {
            _takeDamageMechanic.Enable();
            _shootMechanic.Enable();
        }

        public void Update()
        {
            _shootMechanic.Update();
        }

        public void Disable()
        {
            _takeDamageMechanic.Disable();
            _shootMechanic.Disable();
        }
    }
}