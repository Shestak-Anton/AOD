using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class Cursor : IStartable, IDisposable
    {
        private readonly Camera _activeCamera;
        private readonly MouseInputHandler _mouseInputHandler;
        public Vector3 Position { private set; get; }

        [Inject]
        public Cursor(
            Camera camera, 
            MouseInputHandler mouseInputHandler)
        {
            _activeCamera = camera;
            _mouseInputHandler = mouseInputHandler;
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
            Position = worldPoint;
        }
    }
}