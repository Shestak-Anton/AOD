using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public sealed class Zombie : MonoBehaviour
    {
        [field: SerializeField] public ZombieCore ZombieCore { private set; get; }
        [field: SerializeField] public ZombieVisual ZombieVisual { private set; get; }


        public void Compose(AtomicEntity atomicEntity)
        {
            ZombieCore.Compose(atomicEntity);
        }

        private void Awake()
        {
            ZombieCore.Build();
            ZombieVisual.Build(ZombieCore);

            ZombieVisual.DeathEvent.Subscribe(() => Destroy(gameObject));
        }

        private void OnEnable()
        {
            ZombieCore.Enable();
            ZombieVisual.Enable();
        }

        private void Update()
        {
            ZombieCore.Update();
        }

        private void OnDisable()
        {
            ZombieVisual.Disable();
            ZombieCore.Disable();
        }
    }
}