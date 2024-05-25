using System;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class MouseButtonObserver : IStartable, IDisposable
    {
        private readonly PlayerCoreComponent _playerCoreComponent;
        private readonly MouseInputHandler _mouseInputHandler;

        [Inject]
        public MouseButtonObserver(PlayerCoreComponent playerCoreComponent, MouseInputHandler mouseInputHandler)
        {
            _playerCoreComponent = playerCoreComponent;
            _mouseInputHandler = mouseInputHandler;
        }

        public void Start()
        {
            _mouseInputHandler.OnLBPressed += _playerCoreComponent.ShootRequest.Invoke;
        }

        public void Dispose()
        {
            _mouseInputHandler.OnLBPressed -= _playerCoreComponent.ShootRequest.Invoke;
        }
    }
}