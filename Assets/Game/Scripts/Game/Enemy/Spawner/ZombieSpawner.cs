using Game.Scripts.Config;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Game.Enemy
{
    public sealed class ZombieSpawner : MonoBehaviour
    {
        private EnemyManager _enemyManager;
        private Player _player;
        private GameConfig _gameConfig;

        [Inject]
        public void Build(
            EnemyManager enemyManager,
            Player playerCore,
            GameConfig gameConfig)
        {
            _enemyManager = enemyManager;
            _player = playerCore;
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
            if (!_enemyManager.RequestNewZombie(out var zombie)) return;
            zombie.ZombieCore.LifeComponent.OnDeadEvent.Subscribe(Spawn);
            zombie.Compose(_player);
        }
    }
}