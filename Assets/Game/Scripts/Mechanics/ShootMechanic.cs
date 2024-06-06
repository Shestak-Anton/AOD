using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts
{
    public class ShootMechanic
    {
        private readonly IAtomicVariable<int> _bulletsCount;
        private readonly IAtomicValue<int> _maxBullet;
        private readonly IAtomicValue<float> _restoreInterval;
        private readonly IAtomicEvent _shootRequest;
        private readonly IAtomicFunction<bool> _shootAvailable;
        private readonly IAtomicEvent _shootAvailableEvent;

        private float _lastRestoringTime;

        public ShootMechanic(
            IAtomicVariable<int> bulletsCount,
            IAtomicValue<int> maxBullet,
            IAtomicValue<float> restoreInterval,
            IAtomicEvent shootRequest,
            IAtomicFunction<bool> shootAvailable,
            IAtomicEvent shootAvailableEvent)
        {
            _bulletsCount = bulletsCount;
            _maxBullet = maxBullet;
            _restoreInterval = restoreInterval;
            _shootRequest = shootRequest;
            _shootAvailable = shootAvailable;
            _shootAvailableEvent = shootAvailableEvent;
        }

        public void Enable()
        {
            _shootRequest.Subscribe(OnShootRequested);
        }

        public void Update()
        {
            if (_bulletsCount.Value >= _maxBullet.Value) return;
            if (Time.time - _lastRestoringTime < _restoreInterval.Value) return;

            _bulletsCount.Value += 1;

            _lastRestoringTime = Time.time;
        }

        public void Disable()
        {
            _shootRequest.Unsubscribe(OnShootRequested);
        }

        private void OnShootRequested()
        {
            if (!_shootAvailable.Value) return;
            if (_bulletsCount.Value > 0)
            {
                _shootAvailableEvent.Invoke();
                _bulletsCount.Value -= 1;
            }
        }
    }
}