using Client.Scripts.Services;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    public class CoinPresenter : MonoBehaviour
    {
        private CustomPool<MoneyPickedParticleWrapper> _pool;
        private IMoneyService _moneyService;
        private const int PlayerLayer = 7;

        [Inject]
        public void Construct(CustomPool<MoneyPickedParticleWrapper> objectPool, IMoneyService moneyService)
        {
            _pool = objectPool;
            _moneyService = moneyService;
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
                ReleaseParticle(particle, this.GetCancellationTokenOnDestroy()).Forget();
            }
        }

        public async UniTask ReleaseParticle(MoneyPickedParticleWrapper particle, CancellationToken sc)
        {
            await UniTask.WaitForSeconds(particle.GetComponent<ParticleSystem>().main.duration, ignoreTimeScale: false, PlayerLoopTiming.Update, sc);
            _pool.Release(particle);
        }
    }
}
