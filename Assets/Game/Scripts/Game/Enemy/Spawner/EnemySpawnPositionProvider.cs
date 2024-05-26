using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public static class EnemySpawnPositionProvider
    {
        public static Vector3 GetRandomPositionIn(float areaSize)
        {
            return new Vector3(Random.Range(-areaSize, areaSize), 0f, Random.Range(-areaSize, areaSize));
        }
    }
}