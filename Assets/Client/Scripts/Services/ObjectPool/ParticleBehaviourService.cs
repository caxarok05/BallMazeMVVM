using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Client.Scripts.Services
{
    public class ParticleBehaviourService
    {
        public async UniTask ReleaseParticle<T>(T particle, CustomPool<T> pool, CancellationToken sc = default) where T : MonoBehaviour 
        {
            await UniTask.WaitForSeconds(particle.GetComponent<ParticleSystem>().main.duration, ignoreTimeScale: false, PlayerLoopTiming.Update, sc);
            pool.Release(particle);
        }
    }
}
