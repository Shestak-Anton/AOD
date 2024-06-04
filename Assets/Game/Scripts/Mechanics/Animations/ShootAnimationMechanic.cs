using Atomic.Elements;
using Game.Scripts.Game;
using UnityEngine;

namespace Game.Scripts
{
    public class ShootAnimationMechanic
    {
        private readonly Animator _animator;
        private readonly AnimationDispatcher _animationDispatcher;
        private readonly IAtomicEvent _shootRequest;
        private readonly IAtomicEvent _shootEvent;
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        private const string LISTENER_NAME = "shoot";

        public ShootAnimationMechanic(
            Animator animator,
            AnimationDispatcher animationDispatcher,
            IAtomicEvent shootRequest, 
            IAtomicEvent shootEvent)
        {
            _animator = animator;
            _animationDispatcher = animationDispatcher;
            _shootRequest = shootRequest;
            _shootEvent = shootEvent;
        }

        public void Enable()
        {
            _animationDispatcher.SubscribeOnEvent(LISTENER_NAME, _shootEvent.Invoke);
            _shootRequest.Subscribe(OnShoot);
        }

        public void Disable()
        {
            _animationDispatcher.UnsubscribeOnEvent(LISTENER_NAME, _shootEvent.Invoke);
            _shootRequest.Unsubscribe(OnShoot);
        }

        private void OnShoot()
        {
            _animator.SetTrigger(Shoot);
        }
    }
}