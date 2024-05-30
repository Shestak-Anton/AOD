using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public sealed class MeleeAttackMechanic
    {
        private readonly IAtomicValue<Vector3> _targetPosition;
        private readonly IAtomicValue<Vector3> _currentPosition;
        private readonly IAtomicValue<float> _attackDistance;
        private readonly IAtomicEvent _attackEvent;

        public MeleeAttackMechanic(
            IAtomicValue<Vector3> targetPosition,
            IAtomicValue<Vector3> currentPosition,
            IAtomicValue<float> attackDistance,
            IAtomicEvent attackEvent)
        {
            _targetPosition = targetPosition;
            _currentPosition = currentPosition;
            _attackDistance = attackDistance;
            _attackEvent = attackEvent;
        }

        public void Update()
        {
            var distance = Vector3.Distance(_targetPosition.Value, _currentPosition.Value);
            if (distance <= _attackDistance.Value)
            {
                _attackEvent.Invoke();
            }
        }
    }
}