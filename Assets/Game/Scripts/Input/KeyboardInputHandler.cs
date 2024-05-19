using Atomic.Elements;
using UnityEngine;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public interface IInputDirectionProvider
    {
        IAtomicObservable<Vector3> Direction { get; }
    }

    public sealed class KeyboardInputHandler : ITickable, IInputDirectionProvider
    {
        public IAtomicObservable<Vector3> Direction => _direction;

        private readonly AtomicVariable<Vector3> _direction = new();

        public void Tick()
        {
            var result = Vector3.zero;
            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                result += Vector3.left;
            }

            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                result += Vector3.forward;
            }

            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                result += Vector3.right;
            }

            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                result += Vector3.back;
            }

            _direction.Value = result;
        }
    }
}