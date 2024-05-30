using System;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class MouseButtonObserver : IStartable, IDisposable
    {
        private readonly Player _playerCore;
        private readonly MouseInputHandler _mouseInputHandler;

        [Inject]
        public MouseButtonObserver(Player playerCore, MouseInputHandler mouseInputHandler)
        {
            _playerCore = playerCore;
            _mouseInputHandler = mouseInputHandler;
        }

        public void Start()
        {
            _mouseInputHandler.OnLBPressed += _playerCore.PlayerCore.ShootRequest.Invoke;
        }

        public void Dispose()
        {
            _mouseInputHandler.OnLBPressed -= _playerCore.PlayerCore.ShootRequest.Invoke;
        }
    }
}