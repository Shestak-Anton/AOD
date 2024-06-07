using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{

    public static class ZombieApi
    {
        public const string DEATH_EVENT = nameof(DEATH_EVENT);
        public const string TAKE_DAMAGE_EVENT = nameof(TAKE_DAMAGE_EVENT);
    }
    
    public sealed class Zombie : AtomicEntity
    {
        [SerializeField] private ZombieCore _zombieCore;
        [SerializeField] private ZombieVisual _zombieVisual;

        [Get(ZombieApi.DEATH_EVENT)] public IAtomicEvent OnDeathEvent => _zombieCore.LifeComponent.OnDeadEvent;
        [Get(ZombieApi.TAKE_DAMAGE_EVENT)] public IAtomicEvent<int> TakeDamageEvent => _zombieCore.TakeDamageComponent.TakeDamage;
        
        public void Compose(AtomicEntity atomicEntity)
        {
            _zombieCore.Compose(atomicEntity);
        }

        private void Awake()
        {
            _zombieCore.Build();
            _zombieVisual.Build(_zombieCore);

            _zombieVisual.DeathEvent.Subscribe(() => Destroy(gameObject));
        }

        private void OnEnable()
        {
            _zombieCore.Enable();
            _zombieVisual.Enable();
        }

        private void Update()
        {
            _zombieCore.Update();
        }

        private void OnDisable()
        {
            _zombieVisual.Disable();
            _zombieCore.Disable();
        }
    }
}