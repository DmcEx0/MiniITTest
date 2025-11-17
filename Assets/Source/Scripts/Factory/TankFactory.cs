using System.Linq;
using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Factory
{
    public class TankFactory : GameObjectFactory
    {
        private readonly MergedTankConfig _mergedTankConfig = null;
        private readonly GridConfig _gridConfig = null;
        
        private readonly Transform _container = null;
        
        private ObjectPool<MergedTankView> _pool = null;

        public TankFactory(MergedTankConfig mergedTankConfig, GridConfig gridConfig, Transform container)
        {
            _mergedTankConfig = mergedTankConfig;
            _gridConfig = gridConfig;
            _container = container;
        }

        public void Prepare()
        {
            int poolSize = _gridConfig.GridSize.x * _gridConfig.GridSize.y;
            _pool = new ObjectPool<MergedTankView>(_mergedTankConfig.MainTankPrefab, poolSize, _container);
        }

        public bool TryGet(out MergedTankData tankData, int level , CellData freeCell)
        {
            DefaultMergedTankData defaultData = _mergedTankConfig.MergedTanksData.FirstOrDefault(data => data.Level == level);

            if (defaultData == null)
            {
                tankData = null;
                return false;
            }
            
            MergedTankView tank = _pool.Get();
            
            tank.Configure(defaultData.Sprite);
            
            tank.transform.SetParent(freeCell.CellView.transform);
            tank.transform.localPosition = Vector3.zero;
            tank.transform.localScale = Vector3.one;
            
            tankData = new MergedTankData(tank, defaultData.Level, defaultData.Damage);
            
            return true;
        }

        public int GetMaxTankLevel()
        {
            int maxLevel = _mergedTankConfig.MergedTanksData.Max(data => data.Level);
            
            return maxLevel;
        }
    }
}
