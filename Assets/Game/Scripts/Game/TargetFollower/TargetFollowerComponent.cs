using UnityEngine;
using VContainer;

namespace Game.Scripts.Game.TargetFollower
{
    public sealed class TargetFollowerComponent : MonoBehaviour
    {
        private MoveMechanic _moveMechanic;

        private PlayerMovementComponent _playerMovementComponent;

        [Inject]
        public void Build(PlayerMovementComponent playerMovementComponent)
        {
            _playerMovementComponent = playerMovementComponent;
        }

        private void Awake()
        {
            _moveMechanic = new MoveMechanic(_playerMovementComponent.MoveDirection, transform);
        }

        private void Update()
        {
            _moveMechanic.Update(Time.deltaTime);
        }
    }
}