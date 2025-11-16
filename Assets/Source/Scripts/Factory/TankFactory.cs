using MiniIT.Configs;
using MiniIT.Factory;

namespace MiniIT
{
    public class TankFactory : GameObjectFactory
    {
        private readonly MergedTankConfig _mergedTankConfig;

        public TankFactory(MergedTankConfig mergedTankConfig)
        {
            _mergedTankConfig = mergedTankConfig;
        }

        public MergedTankData Get()
        {
            return default;
        }
    }
}
