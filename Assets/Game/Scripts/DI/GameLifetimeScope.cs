using Game.Scripts.Game.Common;
using Game.Scripts.Input;
using VContainer;
using VContainer.Unity;

namespace Game.Scripts.DI
{
    public sealed class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<KeyboardInputHandler>();
            builder.RegisterEntryPoint<InputObserver>();
            
            builder.RegisterComponentInHierarchy<Player>().As<IMovable>();
        }
    }
}