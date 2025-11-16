using MiniIT.Configs;
using MiniIT.Controllers;
using MiniIT.Models;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MiniIT.Scopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private MergedTankConfig _mergedTankConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_gameConfig);
            builder.RegisterComponent(_mergedTankConfig);

            builder.Register<GameFieldModel>(Lifetime.Singleton);
            builder.Register<TanksModel>(Lifetime.Singleton);
            builder.Register<GridModel>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<GameController>();
        }
    }
}
