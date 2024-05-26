using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game
{
    public sealed class TakeDamageComponent : MonoBehaviour
    {
        [field: SerializeField] public AtomicEvent<int> TakeDamage { private set; get; }
    }
}