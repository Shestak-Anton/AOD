using System;
using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game
{
    [Serializable]
    public sealed class TakeDamageComponent
    {
        [field: SerializeField] public AtomicEvent<int> TakeDamage { private set; get; }
    }
}