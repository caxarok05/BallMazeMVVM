using Client.Scripts.Data;
using Client.Scripts.Infrastructure.AssetManagement;
using Client.Scripts.Logic;
using Client.Scripts.Presenters;
using Client.Scripts.Services;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using Zenject;

namespace Client.Scripts.LogicModels
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly int _currentLevel;
        private readonly string _nextLevel;
        private readonly GameConfig _gameConfig;

        //async creation is it normal or not

        public GameFactory(IAssetProvider assetProvider, IJsonDataService dataService, int level, string nextLevel)
        {
            _currentLevel = level;
            _nextLevel = nextLevel;
            _gameConfig = dataService.GameConfig;
            _assetProvider = assetProvider;
        }

        public async void CreateStartPoint()
        {
            await _assetProvider.Instantiate(AssetPath.StartPointPath, new Vector3(_gameConfig.LevelData[_currentLevel].PlayerStartPositon.X, 0.01f, _gameConfig.LevelData[_currentLevel].PlayerStartPositon.Z));

        }

        public async void CreateFinishPoint()
        {
            var point = await _assetProvider.InstantiateComponent<NextLevelPresenter>(AssetPath.FinishPointPath, new Vector3(_gameConfig.LevelData[_currentLevel].PlayerEndPositon.X, _gameConfig.LevelData[_currentLevel].PlayerEndPositon.Y, _gameConfig.LevelData[_currentLevel].PlayerEndPositon.Z));
            point.Init(_nextLevel);
        }

        public async void CreateMoney()
        {
            for (int i = 0; i < _gameConfig.LevelData[_currentLevel].CoinsAmount; i++)
            {
               await _assetProvider.Instantiate(AssetPath.CoinPath, new Vector3(_gameConfig.LevelData[_currentLevel].CoinsDatas[i].X, _gameConfig.LevelData[_currentLevel].CoinsDatas[i].Y, _gameConfig.LevelData[_currentLevel].CoinsDatas[i].Z));
            }
        }

        public void CreateEnemy()
        {
            for (int i = 0; i < _gameConfig.LevelData[_currentLevel].hedgehogAmount; i++)
            {
                var stateMachine = new HedgehogStateMachine();
                Dictionary<int, IState> states = new Dictionary<int, IState>();
                for (int m = 0; m < _gameConfig.LevelData[_currentLevel].HedgehogDatas[i].HedgeHogPoints.Count; m++)
                {
                    states.Add(m, new HedgehogPoint(new Vector3(_gameConfig.LevelData[_currentLevel].HedgehogDatas[i].HedgeHogPoints[m].X, _gameConfig.LevelData[_currentLevel].HedgehogDatas[i].HedgeHogPoints[m].Y, _gameConfig.LevelData[_currentLevel].HedgehogDatas[i].HedgeHogPoints[m].Z), _gameConfig.LevelData[_currentLevel].HedgehogDatas[i].HedgeHogPoints[m].Speed, stateMachine));
                }
                stateMachine.Construct(states);
                CreateHedgehog(stateMachine, new Vector3(_gameConfig.LevelData[_currentLevel].HedgehogDatas[i].HedgeHogPoints[0].X, _gameConfig.LevelData[_currentLevel].HedgehogDatas[i].HedgeHogPoints[0].Y, _gameConfig.LevelData[_currentLevel].HedgehogDatas[i].HedgeHogPoints[0].Z));
            }

        }

        private async void CreateHedgehog(HedgehogStateMachine stateMachine, Vector3 startPoint)
        {
            var hedgehod = await _assetProvider.InstantiateComponent<HedgehogPresenter>(AssetPath.HedgehogPath);
            hedgehod.Init(stateMachine, startPoint);
        }
    }
}
