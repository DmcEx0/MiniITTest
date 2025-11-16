using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Data
{
    public class CellData
    {
        public bool IsBusy { get; private set; }
        public CellView CellView { get; private set; }
        public MergedTankData TankData { get; private set; }
#if ENABLE_DEBUG
        public string Name {get; set;}
#endif
        public CellData(CellView cellView)
        {
            CellView = cellView;
        }

        public void ReleaseBusyTank()
        {
            if (TankData == null)
            {
                IsBusy = false;
                return;
            }
            
            TankData.View.Disable();
            TankData = null;
        }

        public void ChangeTank(MergedTankData tankData)
        {
            ReleaseBusyTank();

            TankData = tankData;
            IsBusy = true;
        }
    }
}