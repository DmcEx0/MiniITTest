using MiniIT.Views;

namespace MiniIT.Data
{
    public class MergedTankData
    {
        private readonly MergedTankView _view;
        
        public readonly int Level;
        public readonly float Damage;

        public IMergeable TankMerged => _view;
        
        public MergedTankData(MergedTankView view, int level, float damage)
        {
            _view = view;
            Level = level;
            Damage = damage;
        }
        
        public void Disable()
        {
            _view.Disable();
        }
    }
}