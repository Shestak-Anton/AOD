using UnityEngine;

namespace Game.Scripts.Config
{
    public sealed class GameConfig : MonoBehaviour
    {
        [field: SerializeField] public int MaxZombieOnScene = 1;
    }
}