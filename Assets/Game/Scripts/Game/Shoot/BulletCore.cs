using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Game.Enemy;
using UnityEngine;

namespace Game.Scripts.Game.Shoot
{
    public sealed class BulletCore : AtomicEntity
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private int _damage = 1;

        public void Build(Vector3 moveDirection)
        {
            _moveComponent.MoveDirection.Value = moveDirection;
            _moveComponent.Speed.Value = _speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out AtomicEntity entity)) return;
            entity.Get<IAtomicEvent<int>>(ZombieApi.TAKE_DAMAGE_EVENT).Invoke(_damage);
            Destroy(gameObject);
        }
    }
}