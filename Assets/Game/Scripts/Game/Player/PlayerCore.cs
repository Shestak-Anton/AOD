using System;
using Atomic.Elements;
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

        private ShootPreparingMechanic _shootPreparingMechanic;
        private TakeDamageMechanic _takeDamageMechanic;


        public void Build(Cursor cursor)
        {
            LookAtComponent.Compose(() => cursor.Position, ()=> !LifeComponent.IsDead.Value);
            var isShootingAvailable = new AtomicFunction<bool>(() => true);
            _shootPreparingMechanic = new ShootPreparingMechanic(
                shootRequest: ShootRequest,
                shootEvent: ShootComponent.ShootEvent,
                isShootingAvailable
            );
            _takeDamageMechanic = new TakeDamageMechanic(TakeDamageComponent.TakeDamage, LifeComponent.Hp);
            MoveComponent.Compose(() => !LifeComponent.IsDead.Value);
        }

        public void Enable()
        {
            _shootPreparingMechanic.Enable();
            _takeDamageMechanic.Enable();
        }

        public void Disable()
        {
            _shootPreparingMechanic.Disable();
            _takeDamageMechanic.Disable();
        }
    }
}