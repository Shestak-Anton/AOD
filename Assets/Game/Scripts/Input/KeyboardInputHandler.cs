using System;
using Atomic.Elements;
using UnityEngine;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class KeyboardInputHandler : ITickable
    {
        public event Action<Vector3> OnDirection;

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

            OnDirection?.Invoke(result);
        }
    }
}