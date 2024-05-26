using Atomic.Elements;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Game.TargetFollower
{
    public sealed class TrackPositionComponent : MonoBehaviour
    {
        [field: SerializeField] public AtomicVariable<Vector3> Position { private set; get; }

        private TrackPositionMechanic _trackPositionMechanic;
        private AtomicVariable<Vector3> _followingPosition;
        
        private DataPositionChangedHandler _dataPositionChangedHandler;
        
        [Inject]
        public void Build(MoveComponent moveComponent)
        {
            _followingPosition = moveComponent.Position;
        }

        private void Awake()
        {
            _dataPositionChangedHandler = new DataPositionChangedHandler(Position, transform);
            
            _trackPositionMechanic = new TrackPositionMechanic(
                followingPosition: _followingPosition,
                controllablePosition: Position
            );
            _trackPositionMechanic.Build(transform.position);
        }

        private void OnEnable()
        {
            _trackPositionMechanic.Enable();
            _dataPositionChangedHandler.Enable();
        }

        private void OnDisable()
        {
            _dataPositionChangedHandler.Disable();
            _trackPositionMechanic.Disable();
        }
    }
}