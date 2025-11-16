using System.Linq;
using MiniIT.Configs;
using MiniIT.Data;
using UnityEngine;

namespace MiniIT.Factory
{
    public class TankFactory : GameObjectFactory
    {
        private readonly MergedTankConfig _mergedTankConfig;

        public TankFactory(MergedTankConfig mergedTankConfig)
        {
            _mergedTankConfig = mergedTankConfig;
        }

        public MergedTankData Get(int level , CellData freeCell)
        {
            var tank = Object.Instantiate(_mergedTankConfig.MergedTanksData[0].TankView, freeCell.CellView.transform);
            tank.transform.localPosition = Vector3.zero;
            
            var defaultData = _mergedTankConfig.MergedTanksData.FirstOrDefault(data => data.Level == level);

            if (defaultData == null)
            {
                return null;
            }
            
            var tankData = new MergedTankData(tank, defaultData.Level, defaultData.Damage);
            
            return tankData;
        }
    }
}
