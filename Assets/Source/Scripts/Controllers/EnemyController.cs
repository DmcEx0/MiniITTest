using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MiniIT.Behaviours;
using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Factory;
using MiniIT.Models;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace MiniIT.Controllers
{
    public class EnemyController : IInitializable, IStartable, IFixedTickable, IDisposable
    {
        private const float DelayBetweenSpawn = 0.5f;

        private readonly GameConfig _gameConfig = null;
        private readonly EnemyModel _enemyModel = null;
        private readonly EnemyFactory _factory = null;
        private readonly MovementSystem _movementSystem = null;
        private readonly Transform _spawnPointsContainer = null;

        private readonly List<Transform> _spawnPoints = null;

        private int _previousIndex = 0;
        private int _currentIndex = 0;

        private bool _canSpawnEnemies = true;

        public EnemyController(EnemyFactory factory, GameConfig gameConfig, EnemyModel enemyModel,
            Transform spawnPointsContainer)
        {
            _factory = factory;
            _gameConfig = gameConfig;
            _enemyModel = enemyModel;
            _movementSystem = new MovementSystem();

            _spawnPointsContainer = spawnPointsContainer;

            _spawnPoints = new List<Transform>();
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
        }

        private async UniTask SpawnEnemiesAsync()
        {
            for (int i = 0; i < _gameConfig.MaxEnemyCount; i++)
            {
                int randomIndex = Random.Range(0, _spawnPoints.Count);
                _currentIndex = randomIndex;

                while (_currentIndex == _previousIndex)
                {
                    _currentIndex = Random.Range(0, _spawnPoints.Count);
                }
                
                _previousIndex = _currentIndex;

                EnemyData data = _factory.Get(_spawnPoints[_currentIndex].position);

                _enemyModel.AddEnemy(data);
                _movementSystem.AddMovable(data.Movable);

                await UniTask.Delay(TimeSpan.FromSeconds(DelayBetweenSpawn));
            }

            while (_canSpawnEnemies)
            {
                await EnableSpawnedEnemiesAsync();
            }
        }

        private async UniTask EnableSpawnedEnemiesAsync()
        {
            _ = _factory.GetOnlyView();

            await UniTask.Delay(TimeSpan.FromSeconds(DelayBetweenSpawn));
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