using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Data
{
    public class CellData
    {
        public bool IsBusy { get; set; }
        public CellView CellView { get; private set; }
        public MergedTankData TankData { get; private set; }
#if ENABLE_DEBUG
        public string Name {get; set;}
#endif
        public CellData(CellView cellView, bool isBusy)
        {
            CellView = cellView;
            IsBusy = isBusy;
        }

        public void ChangeTank(MergedTankData tankData)
        {
            if (TankData != null)
            {
                Object.Destroy(TankData.View.gameObject);
                TankData = null;
            }

            TankData = tankData;
        }
    }
}