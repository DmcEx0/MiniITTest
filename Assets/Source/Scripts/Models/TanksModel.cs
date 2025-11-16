using System.Collections.Generic;
using MiniIT.Views;

namespace MiniIT.Models
{
    public class TanksModel
    {
        private readonly List<MergedTankView> _mergedTanks;

        public TanksModel(List<MergedTankView> mergedTanks)
        {
            _mergedTanks = mergedTanks;
        }

        public IReadOnlyList<MergedTankView> MergedTanks  => _mergedTanks;

        public void AddTank(MergedTankView tank)
        {
            _mergedTanks.Add(tank);
        }
    }
}
