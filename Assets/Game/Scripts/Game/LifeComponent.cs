using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Scripts.Game
{
    [Serializable]
    public sealed class LifeComponent
    {
        [field: SerializeField] public AtomicVariable<int> Hp { private set; get; }
        [field: SerializeField] public AtomicVariable<bool> IsDead { private set; get; }
        [field: SerializeField] public AtomicEvent OnDeadEvent { private set; get; }
        
        public void Build(AtomicObject atomicObject)
        {
            atomicObject.AddLogic(new DeathMechanic(Hp, IsDead, OnDeadEvent));
        }
    }
}