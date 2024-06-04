using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public sealed class MeleeAttackMechanic
    {
        private readonly IAtomicValue<Vector3> _targetPosition;
        private readonly IAtomicValue<Vector3> _currentPosition;
        private readonly IAtomicValue<float> _attackDistance;
        private readonly IAtomicValue<bool> _attackAvailable;
        private readonly IAtomicEvent _attackEvent;

        public MeleeAttackMechanic(
            IAtomicValue<Vector3> targetPosition,
            IAtomicValue<Vector3> currentPosition,
            IAtomicValue<float> attackDistance,
            IAtomicEvent attackEvent,
            IAtomicValue<bool> attackAvailable)
        {
            _targetPosition = targetPosition;
            _currentPosition = currentPosition;
            _attackDistance = attackDistance;
            _attackEvent = attackEvent;
            _attackAvailable = attackAvailable;
        }

        public void Update()
        {
            if (!_attackAvailable.Value) return;
            if (_currentPosition.Value == Vector3.zero && _targetPosition.Value == Vector3.zero) return;
            var distance = Vector3.Distance(_targetPosition.Value, _currentPosition.Value);
            if (distance <= _attackDistance.Value)
            {
                _attackEvent.Invoke();
            }
        }
    }
}