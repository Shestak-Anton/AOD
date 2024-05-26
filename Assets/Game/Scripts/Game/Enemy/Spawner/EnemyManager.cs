using System;
using Atomic.Elements;
using Game.Scripts.Config;
using UnityEngine;
using VContainer;
using Object = UnityEngine.Object;

namespace Game.Scripts.Game.Enemy
{
    public sealed class EnemyManager
    {
        public int ZombieCount => _registry.RegisteredCount;

        private readonly Registry<ZombieCore> _registry = new();
        private readonly int _maxZombieOnScene;

        private readonly Func<Vector3, Quaternion, ZombieCore> _zombieFactory;
        private readonly IAtomicVariable<Vector3> _followingPosition;

        [Inject]
        public EnemyManager(
            Func<Vector3, Quaternion, ZombieCore> zombieFactory,
            GameConfig gameConfig,
            PlayerCoreComponent playerCoreComponent
        )
        {
            _zombieFactory = zombieFactory;
            _maxZombieOnScene = gameConfig.MaxZombieOnScene;
            _followingPosition = playerCoreComponent.MoveComponent.Position;
        }

        public bool RequestNewZombie(out ZombieCore zombie)
        {
            var randomPositionIn = EnemySpawnPositionProvider.GetRandomPositionIn(Vector3.zero, Vector3.zero);
            var zombieInstance =
                _zombieFactory.Invoke(randomPositionIn, Quaternion.LookRotation(_followingPosition.Value));

            zombie = zombieInstance;
            return true;
        }

        public void RemoveZombie(ZombieCore zombieCore)
        {
            Object.Destroy(zombieCore.gameObject);
        }
        
    }
}