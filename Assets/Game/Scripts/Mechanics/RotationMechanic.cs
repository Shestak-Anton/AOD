using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class RotationMechanic
    {
        private readonly Transform _transform;
        private readonly IAtomicObservable<Vector3> _rotation;

        public RotationMechanic(Transform transform, IAtomicObservable<Vector3> rotation)
        {
            _transform = transform;
            _rotation = rotation;
        }

        public void Enable()
        {
            _rotation.Subscribe(OnRotationChanged);
        }

        public void Disable()
        {
            _rotation.Unsubscribe(OnRotationChanged);
        }

        private void OnRotationChanged(Vector3 rotation)
        {
            _transform.rotation = Quaternion.LookRotation(rotation, Vector3.up);
        }
    }
}