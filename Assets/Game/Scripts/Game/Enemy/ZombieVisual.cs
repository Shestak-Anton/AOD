using System;
using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    [Serializable]
    public sealed class ZombieVisual
    {
        [field: SerializeField] public AtomicEvent DeathEvent { private set; get; }

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationDispatcher _animationDispatcher;

        private ZombieCore _zombieCore;

        private MeleeAttackAnimationMechanic _meleeAttackAnimationMechanic;
        private DeathAnimationMechanic _deathAnimationMechanic;
        private MoveAnimationMechanic _moveAnimationMechanic;

        public void Build(ZombieCore zombieCore)
        {
            _zombieCore = zombieCore;

            _meleeAttackAnimationMechanic = new MeleeAttackAnimationMechanic(
                animator: _animator,
                animationDispatcher: _animationDispatcher,
                attackRequest: _zombieCore.AttackRequestEvent,
                attackEvent: zombieCore.AttackTargetEvent,
                damage: zombieCore.AttackDamage
            );
            _deathAnimationMechanic = new DeathAnimationMechanic(
                _animator,
                _animationDispatcher,
                _zombieCore.LifeComponent.OnDeadEvent,
                DeathEvent
            );
            _moveAnimationMechanic = new MoveAnimationMechanic(
                _animator, _zombieCore.MoveComponent.MoveDirection, zombieCore.MoveComponent.Speed
            );
        }

        public void Enable()
        {
            _meleeAttackAnimationMechanic.Enable();
            _deathAnimationMechanic.Enable();
            _moveAnimationMechanic.Enable();
        }

        public void Disable()
        {
            _moveAnimationMechanic.Disable();
            _meleeAttackAnimationMechanic.Disable();
            _deathAnimationMechanic.Disable();
        }
    }
}