using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MiniIT.Behaviours;
using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Factory;
using MiniIT.Models;
using MiniIT.Providers;
using MiniIT.Views;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace MiniIT.Controllers
{
    public class EnemyController : IInitializable, IStartable, IFixedTickable, IDisposable
    {
        private readonly EnemyModel _enemyModel = null;
        private readonly EnemyFactory _factory = null;
        private readonly GameConfig _gameConfig = null;
        private readonly AsyncAnimationProvider _animationProvider = null;
        private readonly MovementSystem _movementSystem = null;
        private readonly Transform _spawnPointsContainer = null;

        private readonly List<Transform> _spawnPoints = null;
        
        private readonly CancellationTokenSource _tokenSource = null;

        private int _previousIndex;
        private int _currentIndex;

        private bool _canSpawnEnemies = true;

        public EnemyController(EnemyFactory factory, EnemyModel enemyModel, Transform spawnPointsContainer,
            GameConfig gameConfig, AsyncAnimationProvider animationProvider)
        {
            _factory = factory;
            _enemyModel = enemyModel;
            _gameConfig = gameConfig;

            _animationProvider = animationProvider;

            _movementSystem = new MovementSystem();

            _spawnPointsContainer = spawnPointsContainer;

            _spawnPoints = new List<Transform>();
            
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _canSpawnEnemies = true;
            _factory.Prepare();

            ConfigureSpawnPoints();
        }

        public void Start()
        {
            SpawnEnemiesAsync().Forget();
        }

        public void FixedTick()
        {
            _movementSystem.UpdateMovements();
        }

        public void Dispose()
        {
            _canSpawnEnemies = false;
            
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }

        private async UniTask SpawnEnemiesAsync()
        {
            while (_canSpawnEnemies)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_gameConfig.DelayBetweenSpawnEnemy));
#if ENABLE_DEBUG
                Debug.Log("Enemies Data Count: " + _enemyModel.EnemiesData.Count);
#endif
                int randomIndex = Random.Range(0, _spawnPoints.Count);
                _currentIndex = randomIndex;

                while (_currentIndex == _previousIndex)
                {
                    _currentIndex = Random.Range(0, _spawnPoints.Count);
                }

                _previousIndex = _currentIndex;

                if (_enemyModel.TryGetAvailableData(out EnemyData enemyData))
                {
                    EnemyView view = _factory.GetOnlyView();

                    enemyData.Init(view);

                    continue;
                }

                EnemyData data = _factory.Get(_spawnPoints[_currentIndex].position);

                _animationProvider.CallMoveEffectAsync(data.Movable.Rigidbody.transform, BehaviourType.Move,
                    _tokenSource.Token).Forget();

                _enemyModel.AddData(data);
                _movementSystem.AddMovable(data.Movable);
            }
        }

        private void ConfigureSpawnPoints()
        {
            int count = _spawnPointsContainer.childCount;

            for (int i = 0; i < count; i++)
            {
                _spawnPoints.Add(_spawnPointsContainer.GetChild(i));
            }
        }
    }
}