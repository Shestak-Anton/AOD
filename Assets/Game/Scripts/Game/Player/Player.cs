using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using VContainer;

namespace Game.Scripts
{
    public static class PlayerApi
    {
        public const string TAKE_DAMAGE_EVENT = nameof(TAKE_DAMAGE_EVENT);
        public const string POSITION_VARIABLE = nameof(POSITION_VARIABLE);
        public const string IS_DEAD_COMPONENT = nameof(IS_DEAD_COMPONENT);
        public const string MOVE_DIRECTION_VARIABLE = nameof(MOVE_DIRECTION_VARIABLE);
        public const string SHOOT_REQUEST_EVENT = nameof(SHOOT_REQUEST_EVENT);
    }

    public sealed class Player : AtomicObject
    {
        [SerializeField] private PlayerCore _playerCore;
        [SerializeField] private PlayerView _playerView;

        [Get(PlayerApi.IS_DEAD_COMPONENT)] public IAtomicVariable<bool> IsDead => _playerCore.LifeComponent.IsDead;

        [Get(PlayerApi.TAKE_DAMAGE_EVENT)]
        public IAtomicEvent<int> TakeDamageEvent => _playerCore.TakeDamageComponent.TakeDamage;

        [Get(PlayerApi.POSITION_VARIABLE)]
        public IAtomicVariable<Vector3> Position => _playerCore.MoveComponent.Position;

        [Get(PlayerApi.MOVE_DIRECTION_VARIABLE)] public IAtomicVariable<Vector3> Direction => _playerCore.MoveComponent.MoveDirection;

        [Get(PlayerApi.SHOOT_REQUEST_EVENT)] public IAtomicEvent ShootRequest => _playerCore.ShootRequest;

        private Func<Vector3> _position;

        [Inject]
        public void Build(Input.Cursor cursor)
        {
            _position = () => cursor.Position;
        }

        private void Awake()
        {
            _playerCore.Build(()=> _position.Invoke(), this);
            _playerView.Build(_playerCore);
        }

        private void OnEnable()
        {
            Enable();
            _playerCore.Enable();
            _playerView.Enable();
        }

        private void Update()
        {
            OnUpdate(Time.deltaTime);
            _playerCore.Update();
        }

        private void OnDisable()
        {
            Disable();
            _playerView.Disable();
            _playerCore.Disable();
        }
    }
}