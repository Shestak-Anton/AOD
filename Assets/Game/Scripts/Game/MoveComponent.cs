using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Game
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [field: SerializeField] public AtomicVariable<Vector3> MoveDirection { private set; get; }
        [field: SerializeField] public AtomicVariable<Vector3> Position { private set; get; }
        [field: SerializeField] public AtomicVariable<float> Speed { private set; get; } = new(1f);
        [field: SerializeField] public AtomicFunction<bool> CanMove { private set; get; } = new(()=> true);


        private MoveMechanic _moveMechanic;
        private DataPositionChangedHandler _dataPositionChangedHandler;

        public void Compose(Func<bool> canMove, AtomicObject atomicObject)
        {
            CanMove.Compose(canMove);
            _moveMechanic = new MoveMechanic(MoveDirection, Speed, Position, CanMove);
            
            atomicObject.AddLogic(_moveMechanic);
        }

        private void Awake()
        {
            Position.Value = transform.position;
            _dataPositionChangedHandler = new DataPositionChangedHandler(Position, transform);

        }


        private void OnEnable()
        {
            _dataPositionChangedHandler.Enable();
        }

        private void OnDisable()
        {
            _dataPositionChangedHandler.Disable();
        }
    }
}