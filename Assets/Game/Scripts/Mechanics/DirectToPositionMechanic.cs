using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game
{
    public sealed class DirectToPositionMechanic
    {
        private readonly IAtomicValue<Vector3> _targetPosition;
        private readonly IAtomicVariable<Vector3> _direction;
        private readonly IAtomicValue<Vector3> _position;

        public DirectToPositionMechanic(
            IAtomicValue<Vector3> targetPosition,
            IAtomicVariable<Vector3> direction,
            IAtomicValue<Vector3> position)
        {
            _targetPosition = targetPosition;
            _direction = direction;
            _position = position;
        }

        public void Update()
        {
            _direction.Value = (_targetPosition.Value - _position.Value).normalized;
        }
    }
}