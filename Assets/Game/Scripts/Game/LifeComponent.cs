using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game
{
    public sealed class LifeComponent : MonoBehaviour
    {
        [field: SerializeField] public AtomicVariable<int> Hp { private set; get; }
        [field: SerializeField] public AtomicVariable<bool> IsDead { private set; get; }

        private DeathMechanic _deathMechanic;

        private void Awake()
        {
            _deathMechanic = new DeathMechanic(Hp, IsDead);
        }

        private void OnEnable()
        {
            _deathMechanic.Enable();
        }

        private void OnDisable()
        {
            _deathMechanic.Disable();
        }
    }
}