using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Game.Enemy
{
    public sealed class ZombieSpawner : MonoBehaviour
    {
        private EnemyManager _enemyManager;

        [Inject]
        public void Build(EnemyManager enemyManager)
        {
            _enemyManager = enemyManager;
        }

        [Button]
        public void Spawn()
        {
            if (_enemyManager.RequestNewZombie(out var zombie))
            {
                zombie.LifeComponent.IsDead.Subscribe(isDead =>
                {
                    if(isDead) Destroy(zombie.gameObject);
                });
            }
        }
    }
}