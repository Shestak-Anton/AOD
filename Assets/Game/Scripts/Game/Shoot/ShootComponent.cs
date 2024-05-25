using Atomic.Elements;
using Game.Scripts.Game.Shoot;
using UnityEngine;

namespace Game.Scripts
{
    public sealed class ShootComponent : MonoBehaviour
    {
        [SerializeField] private BulletCore _bulletCorePrefab;
        [field: SerializeField] public Transform FirePoint { private set; get; }
        [field: SerializeField] public AtomicEvent ShootEvent { private set; get; }


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
            var bullet = Instantiate(
                _bulletCorePrefab,
                FirePoint.position,
                FirePoint.rotation);
            bullet.Build(moveDirection: FirePoint.forward, position: FirePoint.position);
        }
    }
}