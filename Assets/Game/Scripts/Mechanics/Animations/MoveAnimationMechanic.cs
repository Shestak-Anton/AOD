using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public class MoveAnimationMechanic
    {
        private readonly Animator _animator;
        private readonly AtomicVariable<Vector3> _moveDirection;

        private static readonly int Speed = Animator.StringToHash("Speed");

        public MoveAnimationMechanic(Animator animator, AtomicVariable<Vector3> moveDirection)
        {
            _animator = animator;
            _moveDirection = moveDirection;
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
            _animator.SetFloat(Speed, direction.magnitude);
        }
    }
}