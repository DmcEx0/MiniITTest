using UnityEngine;

namespace MiniIT.Factory
{
    public class TankSpawner
    {
        private readonly TankFactory _tankFactory;

        public TankSpawner(TankFactory tankFactory)
        {
            _tankFactory = tankFactory;
        }

        // public void Spawn()
        // {
        //     if (_gridModel.TryGetFreeCell(out CellData freeCell) == false)
        //     {
        //         return;
        //     }
        //
        //     var newTankData = _tankFactory.Get(0, freeCell);
        //
        //     freeCell.IsBusy = true;
        //     freeCell.AddTank(newTankData);
        //
        //     _mergeModel.RegisterNewData(newTankData);
        // }
    }
}
