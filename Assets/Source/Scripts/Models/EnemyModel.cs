using System.Collections.Generic;
using MiniIT.Data;

namespace MiniIT.Models
{
    public class EnemyModel
    {
        public List<EnemyData> _enemiesData = null;

        
        public EnemyModel()
        {
            _enemiesData = new List<EnemyData>();
        }

        public void AddEnemy(EnemyData enemyData)
        {
            _enemiesData.Add(enemyData);
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