using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public sealed class MeleeAttackAnimationMechanic : IAtomicEnable, IAtomicDisable
    {
        private readonly Animator _animator;
        private readonly AnimationDispatcher _animationDispatcher;
        private readonly IAtomicObservable _attackRequest;
        private readonly IAtomicEvent<int> _attackEvent;
        private readonly IAtomicValue<int> _damage;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private const string LISTENER_NAME = "attack";

        public MeleeAttackAnimationMechanic(
            Animator animator,
            AnimationDispatcher animationDispatcher,
            IAtomicObservable attackRequest,
            IAtomicEvent<int> attackEvent, 
            IAtomicValue<int> damage)
        {
            _animator = animator;
            _animationDispatcher = animationDispatcher;
            _attackRequest = attackRequest;
            _attackEvent = attackEvent;
            _damage = damage;
        }

        public void Enable()
        {
            _attackRequest.Subscribe(OnAttackRequest);
            _animationDispatcher.SubscribeOnEvent(LISTENER_NAME, OnAttack);
        }

        public void Disable()
        {
            _attackRequest.Unsubscribe(OnAttackRequest);
            _animationDispatcher.UnsubscribeOnEvent(LISTENER_NAME, OnAttack);
        }

        private void OnAttackRequest()
        {

            _animator.SetTrigger(Attack);
        }

        private void OnAttack()
        {
            _attackEvent.Invoke(_damage.Value);
        }

    }
}