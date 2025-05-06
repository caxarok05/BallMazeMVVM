using Client.Scripts.Data;
using Client.Scripts.Services;
using System;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Presenters
{
    public class BallRespawnPresenter : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] private Animator animator;

        private int _currentLevel;
        private IHealthService _healthService;
        private GameBallPresenter _ballPresenter;
        private GameConfig _gameConfig;
        
        private const string RespawnTrigger = "RespawnBall";
        
        [Inject]
        public void Construct(int level, IJsonDataService dataService, GameBallPresenter ballPresenter, IHealthService healthService)
        {
            _gameConfig = dataService.GameConfig;
            _currentLevel = level;
            _ballPresenter = ballPresenter;
            _healthService = healthService;
        }

        public void Initialize()
        {
            _healthService.healthChanged += RespawnBall;
        }
        public void Dispose()
        {
            _healthService.healthChanged -= RespawnBall;
        }

        public void RespawnBall()
        {
            animator.SetTrigger(RespawnTrigger);
            var playerPos = _gameConfig.LevelData[_currentLevel].PlayerStartPositon;
            _ballPresenter.SetVelocity(Vector3.zero);
            transform.position = new Vector3(playerPos.X, playerPos.Y, playerPos.Z);
            _ballPresenter.SetBallNewPosition(transform.position);
        }


    }
}
