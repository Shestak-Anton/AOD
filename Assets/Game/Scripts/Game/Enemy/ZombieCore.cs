using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    public sealed class ZombieCore : MonoBehaviour
    {
        [field: SerializeField] public LifeComponent LifeComponent { private set; get; }
        [field: SerializeField] public TakeDamageComponent TakeDamageComponent { private set; get; }
        
        private TakeDamageMechanic _takeDamageMechanic;

        private void Awake()
        {
            _takeDamageMechanic = new TakeDamageMechanic(TakeDamageComponent.TakeDamage, LifeComponent.Hp);
        }

        private void OnEnable()
        {
            _takeDamageMechanic.Enable();
        }

        private void OnDisable()
        {
            _takeDamageMechanic.Disable();
        }
    }
}