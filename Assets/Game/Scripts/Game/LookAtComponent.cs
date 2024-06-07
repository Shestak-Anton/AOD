using System;
using UnityEngine;
using Atomic.Elements;
using Atomic.Objects;

namespace Game.Scripts
{
    public sealed class LookAtComponent : MonoBehaviour
    {
        [field: SerializeField] public AtomicEvent<Vector3> RotateAction { private set; get; }
        [field: SerializeField] public AtomicFunction<Vector3> LookAtPoint { private set; get; }
        [field: SerializeField] public AtomicFunction<bool> CanLook { private set; get; }

        public void Build(
            Func<Vector3> lookAtPosition,
            Func<bool> canLook,
            AtomicObject atomicObject)
        {
            LookAtPoint.Compose(lookAtPosition);
            CanLook.Compose(canLook);

            var pointOfViewPosition = new AtomicFunction<Vector3>(() => transform.position);
            atomicObject.AddLogic(
                new LookAtMechanic(RotateAction, LookAtPoint, pointOfViewPosition, CanLook)
            );
            atomicObject.AddLogic(new RotationMechanic(transform, RotateAction));
        }
    }
}