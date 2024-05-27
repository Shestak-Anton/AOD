using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class LookAtMechanic
    {
        private readonly IAtomicEvent<Vector3> _rotateAction;
        private readonly IAtomicFunction<Vector3> _targetPoint;
        private readonly IAtomicFunction<Vector3> _pointOfViewPosition;

        public LookAtMechanic(
            IAtomicEvent<Vector3> rotateAction,
            IAtomicFunction<Vector3> targetPoint,
            IAtomicFunction<Vector3> pointOfViewPosition)
        {
            _rotateAction = rotateAction;
            _targetPoint = targetPoint;
            _pointOfViewPosition = pointOfViewPosition;
        }

        public void Update()
        {
            var direction = _targetPoint.Invoke() - _pointOfViewPosition.Invoke();
            _rotateAction.Invoke(direction.normalized);
        }
    }
}