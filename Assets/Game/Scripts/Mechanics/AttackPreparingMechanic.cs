using Atomic.Elements;

namespace Game.Scripts
{
    public sealed class AttackPreparingMechanic
    {
        private readonly IAtomicObservable _attackRequest;
        private readonly IAtomicEvent _attackEvent;
        private readonly IAtomicFunction<bool> _isAttackAvailable;

        public AttackPreparingMechanic(
            IAtomicObservable attackRequest,
            IAtomicEvent attackEvent,
            IAtomicFunction<bool> isAttackAvailable)
        {
            _attackRequest = attackRequest;
            _attackEvent = attackEvent;
            _isAttackAvailable = isAttackAvailable;
        }

        public void Enable()
        {
            _attackRequest.Subscribe(OnShoot);
        }

        public void Disable()
        {
            _attackRequest.Unsubscribe(OnShoot);
        }

        private void OnShoot()
        {
            if (!_isAttackAvailable.Invoke()) return;
            _attackEvent.Invoke();
        }
    }
}