using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class InputDirectionObserver : IStartable, IDisposable
    {
        private readonly KeyboardInputHandler _keyboardInputHandler;
        private readonly Player _player;

        [Inject]
        public InputDirectionObserver(KeyboardInputHandler keyboardInputHandler, Player player)
        {
            _keyboardInputHandler = keyboardInputHandler;
            _player = player;
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
            _player.PlayerCore.MoveComponent.MoveDirection.Value = inputDirection;
        }
    }
}