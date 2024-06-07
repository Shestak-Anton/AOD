using System;
using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game
{
    [Serializable]
    public sealed class LifeComponent
    {
        [field: SerializeField] public AtomicVariable<int> Hp { private set; get; }
        [field: SerializeField] public AtomicVariable<bool> IsDead { private set; get; }
        [field: SerializeField] public AtomicEvent OnDeadEvent { private set; get; }

        private DeathMechanic _deathMechanic;

        public void Build()
        {
            _deathMechanic = new DeathMechanic(Hp, IsDead, OnDeadEvent);
        }

        public void Enable()
        {
            _deathMechanic.Enable();
        }

        public void Disable()
        {
            _deathMechanic.Disable();
        }
    }
}