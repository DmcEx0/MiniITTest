using MiniIT.Configs;
using MiniIT.Controllers;
using MiniIT.Factory;
using MiniIT.Input;
using MiniIT.Models;
using MiniIT.Tools;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MiniIT.Scopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private MergedTankConfig _mergedTankConfig;
        [SerializeField] private GridConfig _gridConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_gameConfig);
            builder.RegisterComponent(_mergedTankConfig);
            builder.RegisterComponent(_gridConfig);

            builder.Register<GridModel>(Lifetime.Singleton);
            builder.Register<MergeModel>(Lifetime.Singleton);
            
            builder.Register<TankFactory>(Lifetime.Singleton);
            builder.Register<AsyncAnimationProvider>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<GameController>();
            builder.RegisterEntryPoint<MergeController>();
            builder.RegisterEntryPoint<PlayerInputController>();
        }
    }
}
