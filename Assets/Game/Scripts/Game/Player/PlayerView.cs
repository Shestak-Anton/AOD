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

        private PlayerCore _playerCore;

        private DeathAnimationMechanic _deathAnimationMechanic;
        private ShootAnimationMechanic _shootAnimationMechanic;
        private MoveAnimationMechanic _moveAnimationMechanic;

        public void Build(PlayerCore playerCore)
        {
            _playerCore = playerCore;
            _deathAnimationMechanic = new DeathAnimationMechanic(
                _animator,
                _animationDispatcher,
                _playerCore.LifeComponent.OnDeadEvent,
                DeathEvent
            );
            _shootAnimationMechanic = new ShootAnimationMechanic(
                _animator, 
                _animationDispatcher, 
                _playerCore.ShootComponent.ShootEvent,
                _playerCore.ShootComponent.OnShoot
            );
            _moveAnimationMechanic = new MoveAnimationMechanic(
                _animator, 
                _playerCore.MoveComponent.MoveDirection,
                _playerCore.MoveComponent.Speed);
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