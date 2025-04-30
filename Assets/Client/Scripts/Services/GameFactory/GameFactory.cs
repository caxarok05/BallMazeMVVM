using Client.Scripts.Data;
using Client.Scripts.Services;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.Scripts.LogicModels
{
    public class GameFactory : IGameFactory
    {
        //make AssetProvider insted of ctor
        public HedgehogView hedgehogPrefab;
        private readonly IJsonDataService _dataService;

        public GameFactory(HedgehogView hedgehogPrefab, IJsonDataService dataService)
        {
            this.hedgehogPrefab = hedgehogPrefab;
            _dataService = dataService;
        }

        public void CreateEnemy()
        {
            var config = _dataService.LoadData<GameConfig>();

            for (int i = 0; i < config.LevelData[0].hedgehogAmount; i++)
            {
                var stateMachine = new HedgehogStateMachine();
                Dictionary<int, IState> states = new Dictionary<int, IState>();
                for (int m = 0; m < config.LevelData[0].HedgehogDatas[i].HedgeHogPoints.Count; m++)
                {
                    states.Add(m, new HedgehogPoint(new Vector3(config.LevelData[0].HedgehogDatas[i].HedgeHogPoints[m].X, config.LevelData[0].HedgehogDatas[i].HedgeHogPoints[m].Y, config.LevelData[0].HedgehogDatas[i].HedgeHogPoints[m].Z), config.LevelData[0].HedgehogDatas[i].HedgeHogPoints[m].Speed, stateMachine));
                }
                stateMachine.Construct(states);
                CreateHedgehog(stateMachine, new Vector3(config.LevelData[0].HedgehogDatas[i].HedgeHogPoints[0].X, config.LevelData[0].HedgehogDatas[i].HedgeHogPoints[0].Y, config.LevelData[0].HedgehogDatas[i].HedgeHogPoints[0].Z));
            }

        }

        private void CreateHedgehog(HedgehogStateMachine stateMachine, Vector3 startPoint)
        {
            HedgehogView hedgehod = Object.Instantiate(hedgehogPrefab);
            hedgehod.Construct(stateMachine, startPoint);
        }
    }
}
