using System;
using UnityEngine;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    public sealed class CursorInputHandler : ITickable
    {
        public event Action<Vector3> OnScreenPosition;

        public void Tick()
        {
            var position = UnityEngine.Input.mousePosition;
            OnScreenPosition?.Invoke(position);
        }
    }
}