using System;
using JetBrains.Annotations;
using UnityEngine;
using VContainer.Unity;

namespace Game.Scripts.Input
{
    [UsedImplicitly]
    public sealed class MouseInputHandler : ITickable
    {
        public event Action<Vector3> OnScreenPosition;
        public event Action OnLBPressed;

        public void Tick()
        {
            var position = UnityEngine.Input.mousePosition;
            OnScreenPosition?.Invoke(position);
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
            {
                OnLBPressed?.Invoke();
            }
        }
    }
}