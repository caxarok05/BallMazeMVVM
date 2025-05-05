using Client.Scripts.Services;
using MVVM;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Client.Scripts.UI
{
    public class DeathPanelViewModel : IInitializable, IDisposable
    {
        [Data("GoToMenu")]
        public readonly ReactiveProperty<bool> Interactable = new();

        private readonly IHealthService _healthService;
        private readonly SceneLoader _sceneLoader;

        private const string _menuName = "MainMenu";

        public DeathPanelViewModel(IHealthService healthService, SceneLoader sceneLoader)
        {
            _healthService = healthService;
            _sceneLoader = sceneLoader;
        }


        public void Initialize()
        {
            _healthService.gameEnded += () => ShowPanel(true);
        }
        public void Dispose()
        {
            _healthService.gameEnded -= () => ShowPanel(true);
        }

        [Method("RestartButton")]
        public void RestartGame()
        {
            _sceneLoader.RestartScene();
        }

        [Method("MenuButton")]
        public void GoToMenu()
        {
            _sceneLoader.Load(_menuName);
        }

        private void ShowPanel(bool show)
        {
            Interactable.Value = show;
        }
    }
}

