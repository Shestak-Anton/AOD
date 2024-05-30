using Atomic.Elements;

namespace Game.Scripts.Game
{
    public sealed class DeathMechanic
    {
        private readonly IAtomicObservable<int> _hp;
        private readonly IAtomicVariable<bool> _isDead;
        private readonly IAtomicEvent _deadEvent;

        public DeathMechanic(IAtomicObservable<int> hp, IAtomicVariable<bool> isDead, IAtomicEvent deadEvent)
        {
            _hp = hp;
            _isDead = isDead;
            _deadEvent = deadEvent;
        }

        public void Enable()
        {
            _hp.Subscribe(OnHpChanged);
        }

        public void Disable()
        {
            _hp.Unsubscribe(OnHpChanged);
        }

        private void OnHpChanged(int hp)
        {
            if (hp == 0 && !_isDead.Value)
            {
                _deadEvent.Invoke();
            }

            _isDead.Value = hp == 0;
        }
    }
}