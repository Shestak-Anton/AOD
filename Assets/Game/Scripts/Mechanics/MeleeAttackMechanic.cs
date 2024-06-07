using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public sealed class MeleeAttackMechanic : IAtomicUpdate
    {
        private readonly IAtomicValue<Vector3> _targetPosition;
        private readonly IAtomicValue<Vector3> _currentPosition;
        private readonly IAtomicValue<float> _attackDistance;
        private readonly IAtomicValue<bool> _attackAvailable;
        private readonly IAtomicValue<float> _attackInterval;
        private readonly IAtomicEvent _attackEvent;

        private float _lastDamage;

        public MeleeAttackMechanic(
            IAtomicValue<Vector3> targetPosition,
            IAtomicValue<Vector3> currentPosition,
            IAtomicValue<float> attackDistance,
            IAtomicEvent attackEvent,
            IAtomicValue<bool> attackAvailable,
            IAtomicValue<float> attackInterval)
        {
            _targetPosition = targetPosition;
            _currentPosition = currentPosition;
            _attackDistance = attackDistance;
            _attackEvent = attackEvent;
            _attackAvailable = attackAvailable;
            _attackInterval = attackInterval;
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_attackAvailable.Value) return;
            if (_currentPosition.Value == Vector3.zero && _targetPosition.Value == Vector3.zero) return;
            var distance = Vector3.Distance(_targetPosition.Value, _currentPosition.Value);
            if (distance <= _attackDistance.Value && Time.time - _lastDamage >= _attackInterval.Value)
            {
                _attackEvent.Invoke();
                _lastDamage = Time.time;
            }
        }
    }
}