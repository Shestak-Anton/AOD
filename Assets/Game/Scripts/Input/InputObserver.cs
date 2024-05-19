using System;
using Game.Scripts.Game.Common;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class InputObserver : IStartable, IDisposable
    {
        private readonly IInputDirectionProvider _inputDirectionProvider;
        private readonly IMovable _movableTarget;

        [Inject]
        public InputObserver(IMovable movableTarget, IInputDirectionProvider inputDirectionProvider)
        {
            _movableTarget = movableTarget;
            _inputDirectionProvider = inputDirectionProvider;
        }

        public void Start()
        {
            _inputDirectionProvider.Direction.Subscribe(OnInputDirectionChanged);
        }

        public void Dispose()
        {
            _inputDirectionProvider.Direction.Unsubscribe(OnInputDirectionChanged);
        }

        private void OnInputDirectionChanged(Vector3 inputDirection)
        {
            _movableTarget.Direction.Value = inputDirection;
        }
    }
}