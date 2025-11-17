using System.Linq;
using MiniIT.MergeTwo.Configs;
using MiniIT.MergeTwo.Data;
using MiniIT.MergeTwo.Views;
using UnityEngine;

namespace MiniIT.MergeTwo.Factory
{
    public class TankFactory : GameObjectFactory
    {
        private readonly MergedTankConfig _mergedTankConfig = null;
        private readonly ObjectPool<MergedTankView> _pool = null;

        public TankFactory(MergedTankConfig mergedTankConfig, GridConfig gridConfig, Transform container)
        {
            _mergedTankConfig = mergedTankConfig;

            int poolSize = gridConfig.GridSize.x * gridConfig.GridSize.y;
            _pool = new ObjectPool<MergedTankView>(_mergedTankConfig.MainTankPrefab, poolSize, container, Create);
        }

        public override void Prepare()
        {
            _pool.Initialize();
        }

        public bool TryGet(out MergedTankData tankData, int level, CellData freeCell)
        {
            DefaultMergedTankData defaultData =
                _mergedTankConfig.MergedTanksData.FirstOrDefault(data => data.Level == level);

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

            tankData = new MergedTankData(tank, defaultData.Level, defaultData.Damage,
                defaultData.DelayBetweenShoot);

            return true;
        }

        public int GetMaxTankLevel()
        {
            int maxLevel = _mergedTankConfig.MergedTanksData.Max(data => data.Level);

            return maxLevel;
        }
    }
}