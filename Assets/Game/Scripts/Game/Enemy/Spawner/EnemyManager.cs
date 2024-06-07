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
        private readonly Func<Vector3, Quaternion, Zombie> _zombieFactory;

        [Inject]
        public EnemyManager(
            Func<Vector3, Quaternion, Zombie> zombieFactory,
            GameConfig gameConfig
        )
        {
            _zombieFactory = zombieFactory;
        }

        public bool RequestNewZombie(out Zombie zombie)
        {
            var randomPositionIn = EnemySpawnPositionProvider.GetRandomPositionIn(20f);
            var zombieInstance =
                _zombieFactory.Invoke(randomPositionIn, Quaternion.LookRotation(Vector3.up));

            zombie = zombieInstance;
            return true;
        }
        
    }
}