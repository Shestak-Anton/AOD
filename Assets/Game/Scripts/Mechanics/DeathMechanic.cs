using Atomic.Elements;

namespace Game.Scripts.Game
{
    public sealed class DeathMechanic
    {
        private readonly IAtomicObservable<int> _hp;
        private readonly IAtomicVariable<bool> _isDead;

        public DeathMechanic(IAtomicObservable<int> hp, IAtomicVariable<bool> isDead)
        {
            _hp = hp;
            _isDead = isDead;
        }

        public void Enable()
        {
            _hp.Subscribe(OnTakeDamage);
        }

        public void Disable()
        {
            _hp.Unsubscribe(OnTakeDamage);
        }

        private void OnTakeDamage(int hp)
        {
            _isDead.Value = hp == 0;
        }
    }
}