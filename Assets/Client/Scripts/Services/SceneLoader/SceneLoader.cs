using Client.Scripts.Presenters;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace Client.Scripts.Services
{
    public class SceneLoader
    {
        private readonly CurtainPresenter _curtain;
        private readonly ZenjectSceneLoader _diContainer;

        public SceneLoader(CurtainPresenter curtain, ZenjectSceneLoader diContainer)
        {
            _curtain = curtain;
            _diContainer = diContainer;
        }

        public void Load(string name, Action onLoaded = null)
        {
            _curtain.Show();
            if (SceneManager.GetActiveScene().name == name)
                onLoaded?.Invoke();
            else
                LoadScene(name, onLoaded).Forget();
        }
        public async void RestartScene()
        {
            _curtain.Show();
            await LoadScene(SceneManager.GetActiveScene().name);
            _curtain.Hide();
        }

        private async UniTask LoadScene(string nextScene, Action onLoaded = null)
        {
            await _diContainer.LoadSceneAsync(nextScene).ToUniTask();
            onLoaded?.Invoke();
            _curtain.Hide();
        }
    }
}
