using System.Collections.Generic;
using MiniIT.Data;

namespace MiniIT.Models
{
    public class EnemyModel
    {
        private List<EnemyData> _enemiesData = null;

        public EnemyModel()
        {
            _enemiesData = new List<EnemyData>();
        }

        public void AddEnemy(EnemyData enemyData)
        {
            _enemiesData.Add(enemyData);
        }
    }
}