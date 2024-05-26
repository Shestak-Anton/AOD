using System;
using Atomic.Elements;
using Game.Scripts.Game.Shoot;
using UnityEngine;
using VContainer;

namespace Game.Scripts
{
    public sealed class ShootComponent : MonoBehaviour
    {
        [field: SerializeField] public Transform FirePoint { private set; get; }
        [field: SerializeField] public AtomicEvent ShootEvent { private set; get; }

        private Func<Vector3, Quaternion, BulletCore> _bulletFactory;

        [Inject]
        public void Build(Func<Vector3, Quaternion, BulletCore> bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        private void OnEnable()
        {
            ShootEvent.Subscribe(Shoot);
        }

        private void OnDisable()
        {
            ShootEvent.Unsubscribe(Shoot);
        }

        private void Shoot()
        {
            var bullet = _bulletFactory.Invoke(FirePoint.position, FirePoint.rotation);
            bullet.Build(moveDirection: FirePoint.forward, position: FirePoint.position);
        }
    }
}