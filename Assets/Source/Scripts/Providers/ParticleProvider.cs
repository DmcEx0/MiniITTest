using Cysharp.Threading.Tasks;
using MiniIT.Configs;
using MiniIT.Factory;
using MiniIT.Views;
using UnityEngine;

namespace MiniIT.Providers
{
    public class ParticleProvider
    {
        private readonly ParticleFactory _factory = null;
        
        public ParticleProvider(ParticleFactory factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            _factory.Prepare();
        }
        
        public async UniTask PlayWaitDisableAsync(Vector3 position)
        {
            ParticleView particle = _factory.Get(BehaviourType.Merge);
            particle.transform.position = position;

            particle.Particle.Play();

            await UniTask.WaitForSeconds(particle.Particle.main.duration);
            
            particle.Disable();
        }
    }
}