using MiniIT.Views;

namespace MiniIT.Data
{
    public class MergedTankData
    {
        public readonly MergedTankView View;
        public readonly int Level;
        public readonly float Damage;
        
        public MergedTankData(MergedTankView view, int level, float damage)
        {
            View = view;
            Level = level;
            Damage = damage;
        }
    }
}