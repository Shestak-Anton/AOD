using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public class MoveAnimationMechanic
    {
        private readonly Animator _animator;
        private readonly AtomicVariable<Vector3> _moveDirection;
        private readonly AtomicVariable<float> _moveSpeed;

        private static readonly int Speed = Animator.StringToHash("Speed");

        public MoveAnimationMechanic(Animator animator, AtomicVariable<Vector3> moveDirection, AtomicVariable<float> moveSpeed)
        {
            _animator = animator;
            _moveDirection = moveDirection;
            _moveSpeed = moveSpeed;
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
            // _animator.SetBool(Move, direction != Vector3.zero);
            _animator.SetFloat(Speed, direction.magnitude);
        }
    }
}