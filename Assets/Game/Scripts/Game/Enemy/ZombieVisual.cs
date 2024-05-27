using System;
using UnityEngine;

namespace Game.Scripts.Game.Enemy
{
    [Serializable]
    public sealed class ZombieVisual
    {
        [SerializeField] private Animator _animator;

        private ZombieCore _zombieCore;
        
        public void Compose(ZombieCore zombieCore)
        {
            _zombieCore = zombieCore;
        }

        public void Enable()
        {
            _zombieCore.MoveComponent.MoveDirection.Subscribe(OnDirectionChanged);
        }

        public void Disable()
        {
            _zombieCore.MoveComponent.MoveDirection.Unsubscribe(OnDirectionChanged);
        }

        private void OnDirectionChanged(Vector3 direction)
        {
            // _animator.SetBool("", direction!= Vector3.zero);
        }
    }
}