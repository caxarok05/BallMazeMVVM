using GoogleMobileAds.Api;
using UnityEngine;

namespace Client.Scripts.Services
{
    public class BannerAdService : IAdService
    {
        private const string AndroidGameId = "ca-app-pub-3940256099942544/6300978111";
        private const string IOSGameId = "ca-app-pub-3940256099942544/2934735716";

        private string _gameId;
        private BannerView _bannerView;

        public BannerAdService() => Initialize();

        private void Initialize()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    _gameId = AndroidGameId;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _gameId = IOSGameId;
                    break;
                case RuntimePlatform.WindowsEditor:
                    _gameId = AndroidGameId;
                    break;
                default:
                    _gameId = "unused";
                    Debug.Log("Unsupported platform");
                    break;
            }

            MobileAds.Initialize((initStatus) => { });
        }

        public void ShowAd()
        {
            if (_bannerView == null)
            {
                CreateBannerView();
            }

            var adRequest = new AdRequest();

            _bannerView.LoadAd(adRequest);
        }

        private void CreateBannerView()
        {

            if (_bannerView != null)
            {
                DestroyAd();
            }

            _bannerView = new BannerView(_gameId, AdSize.Banner, AdPosition.Top);
        }

        private void DestroyAd()
        {
            if (_bannerView != null)
            {
                _bannerView.Destroy();
                _bannerView = null;
            }
        }

        private void ListenToAdEvents()
        {
            _bannerView.OnBannerAdLoaded += () =>
            {
                Debug.Log("Banner view loaded an ad with response : "
                    + _bannerView.GetResponseInfo());
            };
            _bannerView.OnBannerAdLoadFailed += (error) =>
            {
                Debug.LogError("Banner view failed to load an ad with error : "
                    + error);
            };
            _bannerView.OnAdPaid += (adValue) =>
            {
                Debug.Log(string.Format("Banner view paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            _bannerView.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Banner view recorded an impression.");
            };
            _bannerView.OnAdClicked += () =>
            {
                Debug.Log("Banner view was clicked.");
            };
            _bannerView.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Banner view full screen content opened.");
            };
            _bannerView.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Banner view full screen content closed.");
            };
        }
    }
}
