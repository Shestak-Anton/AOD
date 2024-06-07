using System;
using Atomic.Elements;
using Atomic.Objects;

namespace Game.Scripts.Game.Enemy
{
    public sealed class TakeDamageMechanic : IAtomicEnable, IAtomicDisable
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
            if (_hp.Value == 0) return;
            _hp.Value = Math.Max(_hp.Value - damage, 0);
        }
    }
}