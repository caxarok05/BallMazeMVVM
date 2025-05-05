using Client.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    public class BannerAdPresenter : MonoBehaviour
    {
        private IAdService _adService;

        [Inject]
        public void Construct(BannerAdService service)
        {
            _adService = service;
        }

        private void Start()
        {
            _adService.ShowAd();
        }
    }
}
