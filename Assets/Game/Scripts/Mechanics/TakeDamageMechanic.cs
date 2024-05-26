using System;
using Atomic.Elements;

namespace Game.Scripts.Game.Enemy
{
    public sealed class TakeDamageMechanic
    {
        private readonly IAtomicObservable<int> _onDamage;
        private readonly IAtomicVariable<int> _hp;

        public TakeDamageMechanic(IAtomicObservable<int> onDamage, IAtomicVariable<int> hp)
        {
            _onDamage = onDamage;
            _hp = hp;
        }

        public void Enable()
        {
            _onDamage.Subscribe(OnTakeDamage);
        }

        public void Disable()
        {
            _onDamage.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int damage)
        {
            _hp.Value = Math.Max(_hp.Value - damage, 0);
        }
    }
}