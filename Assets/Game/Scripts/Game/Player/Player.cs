using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using VContainer;

namespace Game.Scripts
{
    public class PlayerApi
    {
        public const string TAKE_DAMAGE_EVENT = nameof(TAKE_DAMAGE_EVENT);
        public const string POSITION_VARIABLE = nameof(POSITION_VARIABLE);
    }

    public sealed class Player : AtomicEntity
    {
        [field: SerializeField] public PlayerCore PlayerCore { private set; get; }
        [field: SerializeField] public PlayerView PlayerView { private set; get; }

        [Get(PlayerApi.TAKE_DAMAGE_EVENT)]
        public AtomicEvent<int> TakeDamage => PlayerCore.TakeDamageComponent.TakeDamage;

        [Get(PlayerApi.POSITION_VARIABLE)] public AtomicVariable<Vector3> Position => PlayerCore.MoveComponent.Position;

        private Input.Cursor _cursor;

        [Inject]
        public void Build(Input.Cursor cursor)
        {
            _cursor = cursor;
            
        }

        private void Awake()
        {
            PlayerCore.Build(_cursor);
            PlayerView.Build(PlayerCore);
        }

        private void OnEnable()
        {
            PlayerCore.Enable();
            PlayerView.Enable();
        }

        private void OnDisable()
        {
            PlayerView.Disable();
            PlayerCore.Disable();
        }
    }
}