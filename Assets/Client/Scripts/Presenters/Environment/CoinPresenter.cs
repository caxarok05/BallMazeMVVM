using Client.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    public class CoinPresenter : MonoBehaviour
    {
        private CustomPool<MoneyPickedParticleWrapper> _pool;
        private ParticleBehaviourService _particleBehaviour;
        private IMoneyService _moneyService;
        private const int PlayerLayer = 7;

        [Inject]
        public void Construct(CustomPool<MoneyPickedParticleWrapper> objectPool, IMoneyService moneyService, ParticleBehaviourService particleBehaviour)
        {
            _pool = objectPool;
            _moneyService = moneyService;
            _particleBehaviour = particleBehaviour;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == PlayerLayer)
            {
                _moneyService.AddMoney(1);
                gameObject.SetActive(false);
                MoneyPickedParticleWrapper particle = _pool.Get();
                particle.gameObject.transform.position = gameObject.transform.position;
                particle.GetComponent<ParticleSystem>().Play();
                _particleBehaviour.ReleaseParticle(particle, _pool, this.GetCancellationTokenOnDestroy()).Forget();
            }
        }
    }
}
