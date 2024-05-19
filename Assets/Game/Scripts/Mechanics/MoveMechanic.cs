using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class MoveMechanic
    {
        private readonly IAtomicValue<Vector3> _direction;
        private readonly Transform _transform;

        public MoveMechanic(IAtomicValue<Vector3> direction, Transform transform)
        {
            _direction = direction;
            _transform = transform;
        }

        public void Update(float deltaTime)
        {
            _transform.position += _direction.Value * deltaTime;
        }
    }
}