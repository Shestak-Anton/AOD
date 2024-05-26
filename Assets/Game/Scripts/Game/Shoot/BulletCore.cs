using UnityEngine;

namespace Game.Scripts.Game.Shoot
{
    public sealed class BulletCore : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private int _damage = 1;

        public void Build(
            Vector3 moveDirection,
            Vector3 position
        )
        {
            _moveComponent.Build(moveDirection, position, _speed);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TakeDamageComponent takeDamageComponent))
            {
                   takeDamageComponent.TakeDamage.Invoke(_damage);
                   Destroy(gameObject);
            }
        }
    }
}