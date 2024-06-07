using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class InputDirectionObserver : IStartable, IDisposable
    {
        private readonly KeyboardInputHandler _keyboardInputHandler;
        private readonly AtomicEntity _atomicEntity;

        [Inject]
        public InputDirectionObserver(KeyboardInputHandler keyboardInputHandler, AtomicEntity atomicEntity)
        {
            _keyboardInputHandler = keyboardInputHandler;
            _atomicEntity = atomicEntity;
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
            var moveDirection = _atomicEntity.Get<IAtomicVariable<Vector3>>(PlayerApi.MOVE_DIRECTION_VARIABLE);
            moveDirection.Value = inputDirection;
        }
    }
}