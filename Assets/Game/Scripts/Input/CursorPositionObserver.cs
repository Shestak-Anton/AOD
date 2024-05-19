using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class CursorPositionObserver : IStartable, IDisposable
    {
        private readonly Camera _activeCamera;
        private readonly CursorInputHandler _cursorInputHandler;
        private readonly PlayerMovementComponent _playerMovementComponent;

        [Inject]
        public CursorPositionObserver(Camera camera, CursorInputHandler cursorInputHandler, PlayerMovementComponent playerMovementComponent)
        {
            _activeCamera = camera;
            _cursorInputHandler = cursorInputHandler;
            _playerMovementComponent = playerMovementComponent;
        }

        public void Start()
        {
            _cursorInputHandler.OnScreenPosition += OnPositionChanged;
        }

        public void Dispose()
        {
            _cursorInputHandler.OnScreenPosition -= OnPositionChanged;
        }

        private void OnPositionChanged(Vector3 screenPosition)
        {
            var worldPoint = _activeCamera.ScreenPointToRay(screenPosition).GetPoint(20f);
            worldPoint.y = 0f;
            _playerMovementComponent.LookAtPoint.Value = worldPoint;
        }
    }
}