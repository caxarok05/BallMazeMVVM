using Client.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    public class InterstitialAdPresenter : MonoBehaviour
    {
        private IAdService _adService;
        private IHealthService _healthService;

        [Inject]
        public void Construct(InterstitialAdService service, IHealthService healthService)
        {
            _adService = service;
            _healthService = healthService;
        }
        private void Awake() => _healthService.gameEnded += ShowAd;

        private void OnDestroy() => _healthService.gameEnded -= ShowAd;

        private void ShowAd()
        {
            _adService.ShowAd();
        }
    }
}
