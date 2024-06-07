using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Config;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Game.Enemy
{
    public sealed class ZombieSpawner : MonoBehaviour
    {
        private EnemyManager _enemyManager;
        private AtomicEntity _target;
        private GameConfig _gameConfig;

        [Inject]
        public void Build(
            EnemyManager enemyManager,
            AtomicEntity target,
            GameConfig gameConfig)
        {
            _enemyManager = enemyManager;
            _target = target;
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
            zombie.Get<IAtomicEvent>(ZombieApi.DEATH_EVENT).Subscribe(Spawn);
            zombie.Compose(_target);
        }
    }
}