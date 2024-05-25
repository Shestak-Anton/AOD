using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class CursorPositionObserver : IStartable, IDisposable
    {
        private readonly Camera _activeCamera;
        private readonly MouseInputHandler _mouseInputHandler;
        private readonly LookAtComponent _lookAtComponent;

        [Inject]
        public CursorPositionObserver(Camera camera, MouseInputHandler mouseInputHandler, LookAtComponent lookAtComponent)
        {
            _activeCamera = camera;
            _mouseInputHandler = mouseInputHandler;
            _lookAtComponent = lookAtComponent;
        }

        public void Start()
        {
            _mouseInputHandler.OnScreenPosition += OnPositionChanged;
        }

        public void Dispose()
        {
            _mouseInputHandler.OnScreenPosition -= OnPositionChanged;
        }

        private void OnPositionChanged(Vector3 screenPosition)
        {
            var worldPoint = _activeCamera.ScreenPointToRay(screenPosition).GetPoint(20f);
            worldPoint.y = 0f;
            _lookAtComponent.LookAtPoint.Value = worldPoint;
        }
    }
}