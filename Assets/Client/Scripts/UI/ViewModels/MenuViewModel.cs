using Client.Scripts.Services;
using MVVM;
using System;
using UniRx;
using Zenject;

namespace Client.Scripts.UI
{
    public class MenuViewModel
    {
        private readonly SceneLoader _sceneLoader;

        private const string _levelScene = "Level0";

        public MenuViewModel(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        [Method("PlayButton")]
        public void Play()
        {
            _sceneLoader.Load(_levelScene);
        }
    }
}
