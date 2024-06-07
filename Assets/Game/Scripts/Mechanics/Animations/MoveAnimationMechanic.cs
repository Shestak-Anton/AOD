using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public class MoveAnimationMechanic
    {
        private readonly Animator _animator;
        private readonly IAtomicObservable<Vector3> _moveDirection;
        private readonly IAtomicValue<bool> _canMove;

        private static readonly int Speed = Animator.StringToHash("Speed");

        public MoveAnimationMechanic(
            Animator animator,
            IAtomicObservable<Vector3> moveDirection, 
            IAtomicValue<bool> canMove)
        {
            _animator = animator;
            _moveDirection = moveDirection;
            _canMove = canMove;
        }

        public void Enable()
        {
            _moveDirection.Subscribe(OnDirectionChanged);
        }

        public void Disable()
        {
            _moveDirection.Unsubscribe(OnDirectionChanged);
        }

        private void OnDirectionChanged(Vector3 direction)
        {
            if (!_canMove.Value) return;
            _animator.SetFloat(Speed, direction.magnitude);
        }
    }
}