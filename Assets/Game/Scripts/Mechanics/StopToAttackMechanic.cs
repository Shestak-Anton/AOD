using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public class StopToAttackMechanic
    {
        private readonly IAtomicValue<float> _attackDistance;
        private readonly IAtomicVariable<Vector3> _position;
        private readonly IAtomicValue<Vector3> _target;
        private readonly IAtomicVariable<bool> _isAttacking;

        public StopToAttackMechanic(
            IAtomicValue<float> attackDistance,
            IAtomicVariable<Vector3> position,
            IAtomicValue<Vector3> target,
            IAtomicVariable<bool> isAttacking)
        {
            _attackDistance = attackDistance;
            _position = position;
            _target = target;
            _isAttacking = isAttacking;
        }


        public void Update()
        {
            _isAttacking.Value = Vector3.Distance(_position.Value, _target.Value) <= _attackDistance.Value;
        }
    }
}