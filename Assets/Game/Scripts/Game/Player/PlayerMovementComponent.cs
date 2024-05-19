using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class PlayerMovementComponent : MonoBehaviour
    {
        [field: SerializeField] public AtomicVariable<Vector3> MoveDirection { private set; get; }
        [field: SerializeField] public AtomicEvent<Vector3> RotateAction { private set; get; }
        [field: SerializeField] public AtomicVariable<Vector3> LookAtPoint { private set; get; }

        private MoveMechanic _moveMechanic;
        private LookAtMechanic _lookAtMechanic;
        private RotationMechanic _rotationMechanic;

        private void Awake()
        {
            _moveMechanic = new MoveMechanic(MoveDirection, transform);
            _lookAtMechanic = new LookAtMechanic(
                RotateAction,
                LookAtPoint,
                new AtomicFunction<Vector3>(() => transform.position));
            _rotationMechanic = new RotationMechanic(transform, RotateAction);
        }

        private void Update()
        {
            _moveMechanic.Update(Time.deltaTime);
            _lookAtMechanic.Update();
        }

        private void OnEnable()
        {
            _rotationMechanic.Enable();
        }

        private void OnDisable()
        {
            _rotationMechanic.Disable();
        }
    }
}