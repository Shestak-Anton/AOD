using Game.Scripts.Config;
using Game.Scripts.Game;
using Game.Scripts.Game.Enemy;
using Game.Scripts.Game.Shoot;
using Game.Scripts.Game.TargetFollower;
using Game.Scripts.Input;
using Game.Scripts.Spawn;
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

            builder.RegisterComponentInHierarchy<TrackPositionComponent>();
            builder.RegisterComponentInHierarchy<Camera>();
            
            builder.RegisterComponentInHierarchy<GameConfig>();

            BuildPlayer(builder);
            BuildEnemies(builder);
            BuildFactories(builder);
        }

        private static void BuildPlayer(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PlayerCoreComponent>();
            builder.RegisterComponentInHierarchy<LookAtComponent>();
            builder.RegisterComponentInHierarchy<MoveComponent>();
            builder.RegisterComponentInHierarchy<ShootComponent>();
        }
        
        private static void BuildEnemies(IContainerBuilder builder)
        {
            builder.Register<EnemyManager>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<ZombieSpawner>();
        }

        private static void BuildFactories(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PrefabProvider>();
            RegisterFactory<BulletCore>(builder);
            RegisterFactory<ZombieCore>(builder);
        }

        private static void RegisterFactory<T>(IContainerBuilder builder)
            where T : MonoBehaviour
        {
            builder.RegisterFactory<Vector3, Quaternion, T>((resolver) =>
            {
                var bulletCore = resolver.Resolve<PrefabProvider>().GetPrefab<T>();
                return (position, rotation) => resolver.Instantiate(bulletCore, position, rotation);
            }, Lifetime.Singleton);
        }
    }
}