using MiniIT.Views;

namespace MiniIT.Data
{
    public class CellData
    {
        public bool IsBusy {get; set;}
        public CellView CellView {get; private set;}
        
        public CellData(CellView cellView, bool isBusy)
        {
            CellView = cellView;
            IsBusy = isBusy;
        }
    }
}