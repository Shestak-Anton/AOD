using Atomic.Elements;
using Game.Scripts.Game;
using UnityEngine;
using VContainer;
using Cursor = Game.Scripts.Input.Cursor;

namespace Game.Scripts
{
    public sealed class PlayerCoreComponent : MonoBehaviour
    {
        [field: SerializeField] public MoveComponent MoveComponent { private set; get; }
        [field: SerializeField] public ShootComponent ShootComponent { private set; get; }
        [field: SerializeField] public LookAtComponent LookAtComponent { private set; get; }
        [field: SerializeField] public AtomicEvent ShootRequest { private set; get; }

        private ShootPreparingMechanic _shootPreparingMechanic;

        private Cursor _cursor;

        [Inject]
        public void Build(Cursor cursor)
        {
            _cursor = cursor;
        }

        private void Awake()
        {
            LookAtComponent.Compose(() => _cursor.Position);
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