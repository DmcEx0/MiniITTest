using System.Collections.Generic;
using MiniIT.MergeTwo.Data;

namespace MiniIT.MergeTwo.Models
{
    public class BulletModel
    {
        private readonly List<BulletData> _bullestData = null;

        public IReadOnlyList<BulletData> BulletData => _bullestData;
        
        public BulletModel()
        {
            _bullestData = new List<BulletData>();
        }
        
        public void AddData(BulletData bulletData)
        {
            _bullestData.Add(bulletData);
        }
        
        public bool TryGetAvailableData(out BulletData bulletData)
        {
            foreach (var data in _bullestData)
            {
                if (data.Movable.Rigidbody.gameObject.activeSelf)
                {
                    continue;
                }
                
                bulletData = data;
                
                return true;
            }

            bulletData = null;
            
            return false;
        }
    }
}