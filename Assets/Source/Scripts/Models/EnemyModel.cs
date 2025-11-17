using System;
using System.Collections.Generic;
using System.Linq;
using MiniIT.Data;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Models
{
    public class EnemyModel : IDisposable
    {
        private readonly BulletModel _bulletModel = null;
        private readonly List<EnemyData> _enemiesData = null;
        
        public IReadOnlyList<EnemyData>  EnemiesData => _enemiesData;

        public EnemyModel(BulletModel bulletModel)
        {
            _bulletModel = bulletModel;
            _enemiesData = new List<EnemyData>();
        }

        public void Dispose()
        {
            foreach (var data in _enemiesData)
            {
                data.Damageable.DamageReceived -= OnDamageReceived;
            }
        }

        public void AddData(EnemyData enemyData)
        {
            enemyData.Damageable.DamageReceived += OnDamageReceived;

            _enemiesData.Add(enemyData);
        }

        private void OnDamageReceived(BulletView bulletView, EnemyView enemyView)
        {
            BulletData bulletData = null;

            foreach (var data in _bulletModel.BulletData)
            {
                if (ReferenceEquals(data.Transform, bulletView.transform))
                {
                    bulletData = data;

                    EnemyData enemyData = _enemiesData.FirstOrDefault(enmData =>
                        ReferenceEquals(enmData.Transform, enemyView.transform));

                    if (enemyData == null)
                    {
                        break;
                    }

                    enemyData.Health.TakeDamage(bulletData.Damage);

                    break;
                }
            }
        }

        public bool TryGetAvailableData(out EnemyData enemyData)
        {
            foreach (var data in _enemiesData)
            {
                if (data.Movable.Rigidbody.gameObject.activeSelf)
                {
                    continue;
                }

                enemyData = data;

                return true;
            }

            enemyData = null;

            return false;
        }
    }
}