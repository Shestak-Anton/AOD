using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Game
{
    public sealed class DataPositionChangedHandler
    {
        private readonly IAtomicObservable<Vector3> _position;
        private readonly Transform _view;

        public DataPositionChangedHandler(IAtomicObservable<Vector3> position, Transform view)
        {
            _position = position;
            _view = view;
        }

        public void Enable()
        {
            _position.Subscribe(OnPositionChanged);
        }

        public void Disable()
        {
            _position.Unsubscribe(OnPositionChanged);
        }

        private void OnPositionChanged(Vector3 position)
        {
            _view.position = position;
        }
    }
}