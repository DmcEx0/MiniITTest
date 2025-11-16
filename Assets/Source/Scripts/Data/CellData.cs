using MiniIT.Views;

namespace MiniIT.Data
{
    public class CellData
    {
        public bool IsBusy { get; private set; }
        public CellView CellView { get; private set; }
        public MergedTankData TankData { get; private set; }

#if ENABLE_DEBUG
        public string Name { get; set; }
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

            TankData.Disable();
            TankData = null;
            IsBusy = false;
        }

        public void ChangeTank(MergedTankData tankData)
        {
            ReleaseBusyTank();

            TankData = tankData;
            IsBusy = true;
        }
    }
}