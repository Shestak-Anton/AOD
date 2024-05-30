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

        private readonly Registry<Zombie> _registry = new();
        private readonly int _maxZombieOnScene;

        private readonly Func<Vector3, Quaternion, Zombie> _zombieFactory;
        private readonly IAtomicVariable<Vector3> _followingPosition;

        [Inject]
        public EnemyManager(
            Func<Vector3, Quaternion, Zombie> zombieFactory,
            GameConfig gameConfig,
            Player player
        )
        {
            _zombieFactory = zombieFactory;
            _maxZombieOnScene = gameConfig.MaxZombieOnScene;
            _followingPosition = player.PlayerCore.MoveComponent.Position;
        }

        public bool RequestNewZombie(out Zombie zombie)
        {
            var randomPositionIn = EnemySpawnPositionProvider.GetRandomPositionIn(20f);
            var zombieInstance =
                _zombieFactory.Invoke(randomPositionIn, Quaternion.LookRotation(Vector3.up));

            zombie = zombieInstance;
            return true;
        }

        public void RemoveZombie(Zombie zombie)
        {
            Object.Destroy(zombie.gameObject);
        }
        
    }
}