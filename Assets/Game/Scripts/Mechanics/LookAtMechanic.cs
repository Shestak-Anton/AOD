using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class LookAtMechanic : IAtomicUpdate
    {
        private readonly IAtomicEvent<Vector3> _rotateAction;
        private readonly IAtomicFunction<Vector3> _targetPoint;
        private readonly IAtomicFunction<Vector3> _pointOfViewPosition;
        private readonly IAtomicFunction<bool> _canLook;
 
        public LookAtMechanic(
            IAtomicEvent<Vector3> rotateAction,
            IAtomicFunction<Vector3> targetPoint,
            IAtomicFunction<Vector3> pointOfViewPosition, IAtomicFunction<bool> canLook)
        {
            _rotateAction = rotateAction;
            _targetPoint = targetPoint;
            _pointOfViewPosition = pointOfViewPosition;
            _canLook = canLook;
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_canLook.Invoke()) return;
            var direction = _targetPoint.Invoke() - _pointOfViewPosition.Invoke();
            if (direction != Vector3.zero)
            {
                _rotateAction.Invoke(direction.normalized);
            }
        }
    }
}