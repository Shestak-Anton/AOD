using Atomic.Elements;
using Game.Scripts.Game.Common;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Game.Camera
{
    public sealed class Camera : MonoBehaviour
    {
        private MoveMechanic _moveMechanic;
        private IAtomicVariable<Vector3> _direction;

        [Inject]
        public void Build(IMovable followingTarget)
        {
            _direction = followingTarget.Direction;
        }

        private void Awake()
        {
            _moveMechanic = new MoveMechanic(_direction, transform);
        }

        private void Update()
        {
            _moveMechanic.Update(Time.deltaTime);
        }
    }
}