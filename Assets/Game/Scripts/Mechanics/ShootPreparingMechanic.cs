using Atomic.Elements;

namespace Game.Scripts
{
    public sealed class ShootPreparingMechanic
    {
        private readonly IAtomicObservable _shootRequest;
        private readonly IAtomicEvent _shootEvent;
        private readonly IAtomicFunction<bool> _isShootingAvailable;

        public ShootPreparingMechanic(
            IAtomicObservable shootRequest,
            IAtomicEvent shootEvent,
            IAtomicFunction<bool> isShootingAvailable)
        {
            _shootRequest = shootRequest;
            _shootEvent = shootEvent;
            _isShootingAvailable = isShootingAvailable;
        }

        public void Enable()
        {
            _shootRequest.Subscribe(OnShoot);
        }

        public void Disable()
        {
            _shootRequest.Unsubscribe(OnShoot);
        }

        private void OnShoot()
        {
            if (!_isShootingAvailable.Invoke()) return;
            _shootEvent.Invoke();
        }
    }
}