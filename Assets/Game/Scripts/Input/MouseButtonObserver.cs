using System;
using Atomic.Elements;
using Atomic.Objects;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class MouseButtonObserver : IStartable, IDisposable
    {
        private readonly MouseInputHandler _mouseInputHandler;
        private readonly AtomicEntity _atomicEntity;

        [Inject]
        public MouseButtonObserver(
            MouseInputHandler mouseInputHandler,
            AtomicEntity atomicEntity)
        {
            _mouseInputHandler = mouseInputHandler;
            _atomicEntity = atomicEntity;
        }

        public void Start()
        {
            _mouseInputHandler.OnLBPressed += OnShootRequested;
        }

        public void Dispose()
        {
            _mouseInputHandler.OnLBPressed -= OnShootRequested;
        }

        private void OnShootRequested()
        {
            var shootRequest = _atomicEntity.Get<IAtomicEvent>(PlayerApi.SHOOT_REQUEST_EVENT);
            shootRequest.Invoke();
        }
    }
}