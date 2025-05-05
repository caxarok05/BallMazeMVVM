using GoogleMobileAds.Api;
using System;
using UnityEngine;

namespace Client.Scripts.Services
{
    public class InterstitialAdService : IAdService
    {
        private const string AndroidGameId = "ca-app-pub-3940256099942544/1033173712";
        private const string IOSGameId = "ca-app-pub-3940256099942544/4411468910";

        private string _gameId;
        private InterstitialAd _interstitialAd;

        public InterstitialAdService() => Initialize();

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

            MobileAds.Initialize((InitializationStatus initStatus) => { });
        }

        public void ShowAd()
        {
            LoadInterstitialAd();
            if (_interstitialAd != null && _interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                _interstitialAd.Show();
            }
            else
            {
                Debug.LogError("Interstitial ad is not ready yet.");
            }
        }

        private void LoadInterstitialAd()
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }

            Debug.Log("Loading the interstitial ad.");

            var adRequest = new AdRequest();

            InterstitialAd.Load(_gameId, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }

                    Debug.Log("Interstitial ad loaded with response : "
                              + ad.GetResponseInfo());

                    _interstitialAd = ad;
                });
        }

        private void RegisterEventHandlers(InterstitialAd interstitialAd)
        {
            interstitialAd.OnAdPaid += (AdValue adValue) =>
            {
                Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                    adValue.Value,
                    adValue.CurrencyCode));
            };
            interstitialAd.OnAdImpressionRecorded += () =>
            {
                Debug.Log("Interstitial ad recorded an impression.");
            };
            interstitialAd.OnAdClicked += () =>
            {
                Debug.Log("Interstitial ad was clicked.");
            };
            interstitialAd.OnAdFullScreenContentOpened += () =>
            {
                Debug.Log("Interstitial ad full screen content opened.");
            };
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                _interstitialAd.Destroy();
                Debug.Log("Interstitial ad full screen content closed.");
            };
            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("Interstitial ad failed to open full screen content " +
                               "with error : " + error);
            };
        }
    }
}