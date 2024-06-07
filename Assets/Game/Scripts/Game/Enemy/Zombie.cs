using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public static class ZombieApi
    {
        public const string DEATH_EVENT = nameof(DEATH_EVENT);
        public const string TAKE_DAMAGE_EVENT = nameof(TAKE_DAMAGE_EVENT);
        public const string ATTACK_REQUEST_EVENT = nameof(ATTACK_REQUEST_EVENT);
        public const string ATTACK_TARGET_EVENT = nameof(ATTACK_TARGET_EVENT);
        public const string ATTACK_DAMAGE = nameof(ATTACK_DAMAGE);
        public const string MOVE_DIRECTION = nameof(MOVE_DIRECTION);
        public const string CAN_MOVE = nameof(CAN_MOVE);
    }

    public sealed class Zombie : AtomicObject
    {
        [SerializeField] private ZombieCore _zombieCore;
        [SerializeField] private ZombieVisual _zombieVisual;

        [Get(ZombieApi.DEATH_EVENT)] public IAtomicEvent OnDeathEvent => _zombieCore.LifeComponent.OnDeadEvent;

        [Get(ZombieApi.TAKE_DAMAGE_EVENT)]
        public IAtomicEvent<int> TakeDamageEvent => _zombieCore.TakeDamageComponent.TakeDamage;
        [Get(ZombieApi.ATTACK_REQUEST_EVENT)] public IAtomicEvent AttackRequestEvent => _zombieCore.AttackRequestEvent;
        [Get(ZombieApi.ATTACK_TARGET_EVENT)] public IAtomicEvent<int> AttackTargetEvent => _zombieCore.AttackTargetEvent;
        [Get(ZombieApi.ATTACK_DAMAGE)] public IAtomicValue<int> AttackDamage => _zombieCore.AttackDamage;
        [Get(ZombieApi.MOVE_DIRECTION)] public IAtomicObservable<Vector3> MoveDirection => _zombieCore.MoveComponent.MoveDirection;
        [Get(ZombieApi.CAN_MOVE)] public IAtomicFunction<bool> CanMove => _zombieCore.MoveComponent.CanMove;

        public void Compose(AtomicEntity target)
        {
            _zombieCore.Compose(target, this);
        }

        private void Awake()
        {
            _zombieCore.Build(this);
            _zombieVisual.Build(this);

            _zombieVisual.DeathEvent.Subscribe(() => Destroy(gameObject));
        }

        private void OnEnable()
        {
            Enable();
        }

        private void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        private void OnDisable()
        {
            Disable();
        }
    }
}