using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    [Serializable]
    public sealed class ZombieVisual
    {
        [field: SerializeField] public AtomicEvent DeathEvent { private set; get; }

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationDispatcher _animationDispatcher;

        public void Build(AtomicObject atomicObject)
        {
            var attackRequestEvent = atomicObject.Get<IAtomicEvent>(ZombieApi.ATTACK_REQUEST_EVENT);
            var attackTargetEvent = atomicObject.Get<IAtomicEvent<int>>(ZombieApi.ATTACK_TARGET_EVENT);
            var attackDamage = atomicObject.Get<IAtomicValue<int>>(ZombieApi.ATTACK_DAMAGE);
            var deathEvent = atomicObject.Get<IAtomicEvent>(ZombieApi.DEATH_EVENT);
            var moveDirection = atomicObject.Get<IAtomicObservable<Vector3>>(ZombieApi.MOVE_DIRECTION);
            var canMove = atomicObject.Get<IAtomicFunction<bool>>(ZombieApi.CAN_MOVE);

            var meleeAttackAnimationMechanic = new MeleeAttackAnimationMechanic(
                animator: _animator,
                animationDispatcher: _animationDispatcher,
                attackRequest: attackRequestEvent,
                attackEvent: attackTargetEvent,
                damage: attackDamage
            );
            var deathAnimationMechanic = new DeathAnimationMechanic(
                _animator,
                _animationDispatcher,
                deathEvent,
                DeathEvent
            );
            var moveAnimationMechanic = new MoveAnimationMechanic(_animator, moveDirection, canMove);

            atomicObject.AddLogic(meleeAttackAnimationMechanic);
            atomicObject.AddLogic(deathAnimationMechanic);
            atomicObject.AddLogic(moveAnimationMechanic);
        }
    }
}