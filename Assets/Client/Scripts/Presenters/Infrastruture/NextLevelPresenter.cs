using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    public class NextLevelPresenter : MonoBehaviour
    {
        private string _nextLevelName;
        private SignalBus _signalBus;
        private SceneLoader _sceneLoader;

        private const int PlayerLayer = 7;

        [Inject] 
        public void Construct(SignalBus signalBus, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _signalBus = signalBus;
            _signalBus.Subscribe<ReadyToNextLevel>(NextLevelActive);
        }

        public void Init(string nextLevel)
        {
            _nextLevelName = nextLevel;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == PlayerLayer)
            {
                _sceneLoader.Load(_nextLevelName);
            }
        }

        private void NextLevelActive()
        {
            gameObject.SetActive(true);
        }
    }
}
