using System;
using Atomic.Elements;
using Game.Scripts.Game;
using Game.Scripts.Game.Enemy;
using UnityEngine;

namespace Game.Scripts
{
    [Serializable]
    public sealed class PlayerView
    {
        [field: SerializeField] public AtomicEvent DeathEvent { private set; get; }

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationDispatcher _animationDispatcher;


        private DeathAnimationMechanic _deathAnimationMechanic;
        private ShootAnimationMechanic _shootAnimationMechanic;
        private MoveAnimationMechanic _moveAnimationMechanic;

        public void Build(PlayerCore playerCore)
        {
            _deathAnimationMechanic = new DeathAnimationMechanic(
                _animator,
                _animationDispatcher,
                playerCore.LifeComponent.OnDeadEvent,
                DeathEvent
            );
            _shootAnimationMechanic = new ShootAnimationMechanic(
                _animator,
                _animationDispatcher,
                playerCore.ShootComponent.ShootEvent,
                playerCore.ShootComponent.OnShoot
            );
            _moveAnimationMechanic = new MoveAnimationMechanic(
                _animator,
                playerCore.MoveComponent.MoveDirection,
                playerCore.MoveComponent.CanMove
            );
        }

        public void Enable()
        {
            _deathAnimationMechanic.Enable();
            _shootAnimationMechanic.Enable();
            _moveAnimationMechanic.Enable();
        }

        public void Disable()
        {
            _deathAnimationMechanic.Disable();
            _shootAnimationMechanic.Disable();
            _moveAnimationMechanic.Disable();
        }
    }
}