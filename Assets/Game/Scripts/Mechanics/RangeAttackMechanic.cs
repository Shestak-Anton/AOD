using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public class RangeAttackMechanic
    {
        private readonly IAtomicValue<float> _attackDistance;
        private readonly IAtomicVariable<Vector3> _position;
        private readonly IAtomicValue<Vector3> _target;
        private readonly IAtomicVariable<bool> _inAttackRange;

        public RangeAttackMechanic(
            IAtomicValue<float> attackDistance,
            IAtomicVariable<Vector3> position,
            IAtomicValue<Vector3> target,
            IAtomicVariable<bool> inAttackRange)
        {
            _attackDistance = attackDistance;
            _position = position;
            _target = target;
            _inAttackRange = inAttackRange;
        }


        public void Update()
        {
            _inAttackRange.Value = Vector3.Distance(_position.Value, _target.Value) <= _attackDistance.Value;
        }
    }
}