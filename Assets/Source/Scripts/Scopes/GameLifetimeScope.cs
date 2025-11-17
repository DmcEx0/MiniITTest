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
        [SerializeField] private GameConfig _gameConfig = null;
        [SerializeField] private MergedTankConfig _mergedTankConfig = null;
        [SerializeField] private GridConfig _gridConfig = null;
        [SerializeField] private AnimationsConfig _animationsConfig = null;
        [SerializeField] private Transform _poolContainer = null;
        [SerializeField] private ParticleSystem _particleSystem = null;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_gameConfig);
            builder.RegisterComponent(_mergedTankConfig);
            builder.RegisterComponent(_gridConfig);
            builder.RegisterComponent(_animationsConfig);

            builder.Register<GridModel>(Lifetime.Singleton);
            builder.Register<MergeModel>(Lifetime.Singleton);

            builder.Register<TankFactory>(Lifetime.Singleton).WithParameter(_poolContainer);
            builder.Register<AsyncAnimationProvider>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameController>();
            builder.RegisterEntryPoint<MergeController>().WithParameter(_particleSystem);
            builder.RegisterEntryPoint<PlayerInputController>();
        }
    }
}