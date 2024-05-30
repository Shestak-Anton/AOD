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
using Cursor = Game.Scripts.Input.Cursor;

namespace Game.Scripts.DI
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private Transform _world;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<KeyboardInputHandler>().AsSelf();
            builder.RegisterEntryPoint<InputDirectionObserver>();
            builder.RegisterEntryPoint<MouseButtonObserver>();
            builder.RegisterEntryPoint<MouseInputHandler>().AsSelf();
            builder.RegisterEntryPoint<Cursor>().AsSelf();

            builder.RegisterComponentInHierarchy<TrackPositionComponent>();
            builder.RegisterComponentInHierarchy<Camera>();

            builder.RegisterComponentInHierarchy<GameConfig>();

            BuildPlayer(builder);
            BuildEnemies(builder);
            BuildFactories(builder);
        }

        private static void BuildPlayer(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<Player>();
            builder.RegisterComponentInHierarchy<LookAtComponent>();
            builder.RegisterComponentInHierarchy<MoveComponent>();
            builder.RegisterComponentInHierarchy<ShootComponent>();
        }

        private static void BuildEnemies(IContainerBuilder builder)
        {
            builder.Register<EnemyManager>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<ZombieSpawner>();
        }

        private void BuildFactories(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<PrefabProvider>();
            RegisterFactory<BulletCore>(builder);
            RegisterFactory<Zombie>(builder);
        }

        private void RegisterFactory<T>(IContainerBuilder builder)
            where T : MonoBehaviour
        {
            builder.RegisterFactory<Vector3, Quaternion, T>((resolver) =>
            {
                var bulletCore = resolver.Resolve<PrefabProvider>().GetPrefab<T>();
                return (position, rotation) =>
                {
                    var inst = resolver.Instantiate(bulletCore, position, rotation);
                    inst.transform.SetParent(_world);
                    return inst;
                };
            }, Lifetime.Singleton);
        }
    }
}