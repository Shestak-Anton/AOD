using UnityEngine;

namespace Game.Scripts.Game.Shoot
{
    public sealed class BulletCore : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private float _speed = 2f;

        public void Build(
            Vector3 moveDirection,
            Vector3 position
        )
        {
            _moveComponent.Build(moveDirection, position, _speed);
        }
    }
}