using MiniIT.Configs;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Factory
{
    public class ParticleFactory : GameObjectFactory
    {
        private const int StarsPoolSize = 6;

        private readonly ObjectPool<ParticleView> _starsPool = null;

        public ParticleFactory(ParticleConfig config, Transform particleContainer)
        {
            _starsPool = new ObjectPool<ParticleView>(config.StarsParticlePrefab, StarsPoolSize, particleContainer, Create);
        }

        public override void Prepare()
        {
            _starsPool.Initialize();
        }

        public ParticleView Get(BehaviourType behaviourType)
        {
            switch (behaviourType)
            {
                case BehaviourType.Merge:
                    return _starsPool.Get();
                default:
                    return _starsPool.Get();
            }
        }
    }
}