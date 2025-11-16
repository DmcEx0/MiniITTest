using System;

namespace MiniIT.Views
{
    public interface IMergeable
    {
        public Action<MergedTankView, MergedTankView> Merging { get; set; }
    }
}
