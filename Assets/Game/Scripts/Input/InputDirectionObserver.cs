using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class InputDirectionObserver : IStartable, IDisposable
    {
        private readonly KeyboardInputHandler _keyboardInputHandler;
        private readonly PlayerMovementComponent _playerMovementComponent;

        [Inject]
        public InputDirectionObserver(KeyboardInputHandler keyboardInputHandler, PlayerMovementComponent playerMovementComponent)
        {
            _keyboardInputHandler = keyboardInputHandler;
            _playerMovementComponent = playerMovementComponent;
        }

        public void Start()
        {
            _keyboardInputHandler.OnDirection += OnInputDirectionChanged;
        }

        public void Dispose()
        {
            _keyboardInputHandler.OnDirection -= OnInputDirectionChanged;
        }

        private void OnInputDirectionChanged(Vector3 inputDirection)
        {
            _playerMovementComponent.MoveDirection.Value = inputDirection;
        }
    }
}