using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Game;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class DeathAnimationMechanic: IAtomicEnable, IAtomicDisable
    {
        private readonly Animator _animator;
        private readonly AnimationDispatcher _animationDispatcher;
        private readonly IAtomicEvent _deadRequest;
        private readonly IAtomicEvent _onDead;
        
        private static readonly int OnDeath = Animator.StringToHash("Die");
        private const string LISTENER_NAME = "death";

        public DeathAnimationMechanic(
            Animator animator,
            AnimationDispatcher animationDispatcher,
            IAtomicEvent deadRequest, 
            IAtomicEvent onDead)
        {
            _animator = animator;
            _animationDispatcher = animationDispatcher;
            _deadRequest = deadRequest;
            _onDead = onDead;
        }

        public void Enable()
        {
            _deadRequest.Subscribe(OnDeathRequest);
            _animationDispatcher.SubscribeOnEvent(LISTENER_NAME, _onDead.Invoke);
        }

        public void Disable()
        {
            _deadRequest.Unsubscribe(OnDeathRequest);
            _animationDispatcher.UnsubscribeOnEvent(LISTENER_NAME, _onDead.Invoke);
        }

        private void OnDeathRequest()
        {
            _animator.SetTrigger(OnDeath);
        }
        
    }
}