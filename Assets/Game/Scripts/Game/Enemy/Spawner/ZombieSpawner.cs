using System;
using Game.Scripts.Config;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Game.Enemy
{
    public sealed class ZombieSpawner : MonoBehaviour
    {
        private EnemyManager _enemyManager;
        private PlayerCoreComponent _playerCoreComponent;
        private GameConfig _gameConfig;

        [Inject]
        public void Build(
            EnemyManager enemyManager,
            PlayerCoreComponent playerCoreComponent,
            GameConfig gameConfig)
        {
            _enemyManager = enemyManager;
            _playerCoreComponent = playerCoreComponent;
            _gameConfig = gameConfig;
        }

        private void Awake()
        {
            for (var i = 0; i < _gameConfig.MaxZombieOnScene; i++)
            {
                Spawn();
            }
        }

        [Button]
        public void Spawn()
        {
            if (_enemyManager.RequestNewZombie(out var zombie))
            {
                zombie.LifeComponent.IsDead.Subscribe(isDead =>
                {
                    if (!isDead) return;
                    
                    Destroy(zombie.gameObject);
                    Spawn();
                });
                zombie.Build(() => _playerCoreComponent.MoveComponent.Position.Value);
            }
        }
    }
}