using Game.Scripts.Game;
using Game.Scripts.Game.TargetFollower;
using Game.Scripts.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.DI
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<KeyboardInputHandler>().AsSelf();
            builder.RegisterEntryPoint<InputDirectionObserver>();
            builder.RegisterEntryPoint<MouseButtonObserver>();
            builder.RegisterEntryPoint<MouseInputHandler>().AsSelf();
            builder.RegisterEntryPoint<CursorPositionObserver>();
            
            builder.RegisterComponentInHierarchy<TargetFollowerComponent>();
            builder.RegisterComponentInHierarchy<PlayerCoreComponent>();
            builder.RegisterComponentInHierarchy<LookAtComponent>();
            builder.RegisterComponentInHierarchy<MoveComponent>();
            builder.RegisterComponentInHierarchy<Camera>();
        }
    }
}