using System;
using UnityEngine;
using Atomic.Elements;

namespace Game.Scripts
{
    public sealed class LookAtComponent : MonoBehaviour
    {
        [field: SerializeField] public AtomicEvent<Vector3> RotateAction { private set; get; }
        [field: SerializeField] public AtomicFunction<Vector3> LookAtPoint { private set; get; }
        [field: SerializeField] public AtomicFunction<bool> CanLook { private set; get; }

        private LookAtMechanic _lookAtMechanic;
        private RotationMechanic _rotationMechanic;

        public void Build(
            Func<Vector3> lookAtPosition,
            Func<bool> canLook)
        {
            LookAtPoint.Compose(lookAtPosition);
            CanLook.Compose(canLook);
        }

        private void Awake()
        {
            _lookAtMechanic = new LookAtMechanic(
                RotateAction,
                LookAtPoint,
                new AtomicFunction<Vector3>(() => transform.position),
                CanLook);
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