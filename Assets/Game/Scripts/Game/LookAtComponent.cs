using UnityEngine;
using Atomic.Elements;

namespace Game.Scripts
{
    public sealed class LookAtComponent : MonoBehaviour
    {
        [field: SerializeField] public AtomicEvent<Vector3> RotateAction { private set; get; }
        [field: SerializeField] public AtomicVariable<Vector3> LookAtPoint { private set; get; }

        private LookAtMechanic _lookAtMechanic;
        private RotationMechanic _rotationMechanic;

        private void Awake()
        {
            _lookAtMechanic = new LookAtMechanic(
                RotateAction,
                LookAtPoint,
                new AtomicFunction<Vector3>(() => transform.position));
            _rotationMechanic = new RotationMechanic(transform, RotateAction);
        }

        private void Update()
        {
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