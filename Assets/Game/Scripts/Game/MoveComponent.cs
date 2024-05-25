using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [field: SerializeField] public AtomicVariable<Vector3> MoveDirection { private set; get; }
        [field: SerializeField] public AtomicVariable<Vector3> Position { private set; get; }
        [field: SerializeField] public AtomicVariable<float> Speed { private set; get; } = new(1f);

        private MoveMechanic _moveMechanic;
        private DataPositionChangedHandler _dataPositionChangedHandler;

        public void Build(
            Vector3 moveDirection,
            Vector3 position,
            float speed
        )
        {
            MoveDirection.Value = moveDirection;
            Position.Value = position;
            Speed.Value = speed;
        }

        private void Awake()
        {
            _dataPositionChangedHandler = new DataPositionChangedHandler(Position, transform);
            _moveMechanic = new MoveMechanic(MoveDirection, Speed, Position);
        }


        private void OnEnable()
        {
            _dataPositionChangedHandler.Enable();
        }

        private void Update()
        {
            _moveMechanic.Update(Time.deltaTime);
        }

        private void OnDisable()
        {
            _dataPositionChangedHandler.Disable();
        }
    }
}