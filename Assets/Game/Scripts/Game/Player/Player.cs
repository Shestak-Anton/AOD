using Atomic.Elements;
using Game.Scripts.Game.Common;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class Player : MonoBehaviour, IMovable
    {
        public IAtomicVariable<Vector3> Direction => _direction;

        [SerializeField] private AtomicVariable<Vector3> _direction;

        private MoveMechanic _moveMechanic;

        private void Awake()
        {
            _moveMechanic = new MoveMechanic(Direction, transform);
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        private void Update()
        {
            _moveMechanic.Update(Time.deltaTime);
        }
    }
}