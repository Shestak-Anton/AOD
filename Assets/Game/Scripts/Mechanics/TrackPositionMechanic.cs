using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game.TargetFollower
{
    public sealed class TrackPositionMechanic
    {
        private readonly IAtomicObservable<Vector3> _followingPosition;
        private readonly IAtomicVariable<Vector3> _controllablePosition;

        public TrackPositionMechanic(
            IAtomicObservable<Vector3> followingPosition,
            IAtomicVariable<Vector3> controllablePosition)
        {
            _followingPosition = followingPosition;
            _controllablePosition = controllablePosition;
        }

        public void Build(Vector3 controllablePosition)
        {
            _controllablePosition.Value = controllablePosition;
        }

        public void Enable()
        {
            _followingPosition.Subscribe(OnPositionChanged);
        }

        public void Disable()
        {
            _followingPosition.Unsubscribe(OnPositionChanged);
        }

        private void OnPositionChanged(Vector3 position)
        {
            position.y = _controllablePosition.Value.y;
            _controllablePosition.Value = position;
        }
    }
}