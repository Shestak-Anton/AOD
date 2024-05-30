using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public class DamageDealerMechanic
    {
        private readonly IAtomicEvent _attackRequestEvent;
        private readonly IAtomicEvent<int> _takeDamageEvent;
        private readonly IAtomicValue<int> _damage;
        private readonly IAtomicValue<float> _interval;

        private float _lastDamage;

        public DamageDealerMechanic(
            IAtomicEvent attackRequestEvent,
            IAtomicEvent<int> takeDamageEvent,
            IAtomicValue<int> damage, 
            IAtomicValue<float> interval)
        {
            _attackRequestEvent = attackRequestEvent;
            _takeDamageEvent = takeDamageEvent;
            _damage = damage;
            _interval = interval;
        }

        public void Enable()
        {
            _attackRequestEvent.Subscribe(OnAttackRequest);
        }
        
        public void Disable()
        {
            _attackRequestEvent.Unsubscribe(OnAttackRequest);
        }

        private void OnAttackRequest()
        {
            if (Time.time - _lastDamage >= _interval.Value)
            {
                _takeDamageEvent.Invoke(_damage.Value);
                _lastDamage = Time.time;
            }
        }

    }
}