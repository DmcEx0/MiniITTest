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

        public bool TryGet(out MergedTankData tankData, int level , CellData freeCell)
        {
            var defaultData = _mergedTankConfig.MergedTanksData.FirstOrDefault(data => data.Level == level);

            if (defaultData == null)
            {
                tankData = null;
                return false;
            }
            
            var tank = Object.Instantiate(defaultData.TankView, freeCell.CellView.transform);
            tank.transform.localPosition = Vector3.zero;
            
            tankData = new MergedTankData(tank, defaultData.Level, defaultData.Damage);
            
            return true;
        }
    }
}
