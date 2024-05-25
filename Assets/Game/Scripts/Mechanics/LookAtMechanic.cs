using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class LookAtMechanic
    {
        private readonly IAtomicEvent<Vector3> _rotateAction;
        private readonly IAtomicValue<Vector3> _targetPoint;
        private readonly IAtomicValue<Vector3> _pointOfViewPosition;

        public LookAtMechanic(
            IAtomicEvent<Vector3> rotateAction,
            IAtomicValue<Vector3> targetPoint,
            IAtomicValue<Vector3> pointOfViewPosition)
        {
            _rotateAction = rotateAction;
            _targetPoint = targetPoint;
            _pointOfViewPosition = pointOfViewPosition;
        }

        public void Update()
        {
            var direction = _targetPoint.Value - _pointOfViewPosition.Value;
            _rotateAction.Invoke(direction.normalized);
        }
    }
}