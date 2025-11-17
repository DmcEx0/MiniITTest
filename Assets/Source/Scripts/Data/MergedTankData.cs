using MiniIT.MergeTwo.Views;
using UnityEngine;

namespace MiniIT.MergeTwo.Data
{
    public class MergedTankData
    {
        private readonly MergedTankView _view  = null;
        private readonly float _delayBetweenShoot;
        
        public readonly int Level;
        public readonly float Damage;

        private bool _canFire;
        public float AccumulatedTime {get; set;}
        
        public IMergeable TankMerged => _view;
        public Transform Transform => _view.transform;
        
        public MergedTankData(MergedTankView view, int level, float damage, float delayBetweenShoot)
        {
            _view = view;
            Level = level;
            Damage = damage;
            _delayBetweenShoot = delayBetweenShoot;
            AccumulatedTime = 0;
        }
        
        public void Disable()
        {
            _view.Disable();
        }

        public bool CheckCanFire()
        {
            if (AccumulatedTime < _delayBetweenShoot)
            {
                return false;
            }
            
            AccumulatedTime = 0f;
            _canFire = true;
            
            return _canFire;
        }
    }
}