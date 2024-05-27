using System;
using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public sealed class Zombie : MonoBehaviour
    {
        [field:SerializeField] public ZombieCore ZombieCore { private set; get; }
        [field:SerializeField] public ZombieVisual ZombieVisual { private set; get; }


        public void Compose(Func<Vector3> targetPosition)
        {
            ZombieCore.Compose(targetPosition);
        }
        
        private void Awake()
        {
            ZombieCore.Build();
        }

        private void OnEnable()
        {
            ZombieCore.Enable();
        }

        private void Update()
        {
            ZombieCore.Update();
        }

        private void OnDisable()
        {
            ZombieCore.Disable();
        }
    }
}