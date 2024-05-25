using Atomic.Elements;
using Game.Scripts.Game;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class PlayerCoreComponent : MonoBehaviour
    {
        [field: SerializeField] public MoveComponent MoveComponent { private set; get; }
        [field: SerializeField] public ShootComponent ShootComponent { private set; get; }
        
        private ShootPreparingMechanic _shootPreparingMechanic;

        [field: SerializeField] public AtomicEvent ShootRequest { private set; get; }

        private void Awake()
        {
            var isShootingAvailable = new AtomicFunction<bool>(() => true);
            _shootPreparingMechanic = new ShootPreparingMechanic(
                shootRequest: ShootRequest,
                shootEvent: ShootComponent.ShootEvent,
                isShootingAvailable
            );
        }

        private void OnEnable()
        {
            _shootPreparingMechanic.Enable();
        }

        private void OnDisable()
        {
            _shootPreparingMechanic.Disable();
        }
    }
}