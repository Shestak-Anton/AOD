using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class MoveMechanic
    {
        private readonly IAtomicValue<Vector3> _direction;
        private readonly IAtomicValue<float> _speed;
        private readonly IAtomicVariable<Vector3> _position;

        public MoveMechanic(
            IAtomicValue<Vector3> direction,
            IAtomicValue<float> speed,
            IAtomicVariable<Vector3> position)
        {
            _direction = direction;
            _speed = speed;
            _position = position;
        }

        public void Update(float deltaTime)
        {
            _position.Value += _direction.Value * (deltaTime * _speed.Value);
        }
    }
}