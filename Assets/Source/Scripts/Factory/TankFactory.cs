using System.Linq;
using MiniIT.Configs;
using MiniIT.Data;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Factory
{
    public class TankFactory : GameObjectFactory
    {
        private readonly MergedTankConfig _mergedTankConfig;
        private readonly GridConfig _gridConfig;
        
        private readonly Transform _container;
        
        private ObjectPool<MergedTankView> _pool;

        public TankFactory(MergedTankConfig mergedTankConfig, GridConfig gridConfig, Transform container)
        {
            _mergedTankConfig = mergedTankConfig;
            _gridConfig = gridConfig;
            _container = container;
        }

        public void Prepare()
        {
            var poolSize = _gridConfig.GridSize.x * _gridConfig.GridSize.y;
            _pool = new ObjectPool<MergedTankView>(_mergedTankConfig.MainTankPrefab, poolSize, _container);
        }

        public bool TryGet(out MergedTankData tankData, int level , CellData freeCell)
        {
            var defaultData = _mergedTankConfig.MergedTanksData.FirstOrDefault(data => data.Level == level);

            if (defaultData == null)
            {
                tankData = null;
                return false;
            }
            
            var tank = _pool.Get();
            
            tank.Configure(defaultData.Sprite);
            
            tank.transform.SetParent(freeCell.CellView.transform);
            tank.transform.localPosition = Vector3.zero;
            tank.transform.localScale = Vector3.one;
            
            tankData = new MergedTankData(tank, defaultData.Level, defaultData.Damage);
            
            return true;
        }

        public int GetMaxTankLevel()
        {
            var maxLevel = _mergedTankConfig.MergedTanksData.Max(data => data.Level);
            
            return maxLevel;
        }
    }
}
