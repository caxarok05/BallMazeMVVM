using Client.Scripts.Data;
using Client.Scripts.Infrastructure.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Services
{
    public class MoneyService : IMoneyService
    {
        public int Money { get; private set; }
        public int MoneyToNextLevel { get; private set; } = 1;


        private readonly SignalBus _signalBus;
        private readonly GameConfig _gameConfig;
        public MoneyService(IJsonDataService dataService, SignalBus signalBus, int level)
        {
            _gameConfig = dataService.GameConfig;
            _signalBus = signalBus;
            SetMoneyToNextLevel(level);
        }

        public void AddMoney(int money)
        {
            Money += money;
            _signalBus.TryFire<MoneyChanged>();
            if (Money >= MoneyToNextLevel)
            {
                _signalBus.TryFire<ReadyToNextLevel>();
            }
        }

        private void SetMoneyToNextLevel(int level)
        {
            MoneyToNextLevel = _gameConfig.LevelData[level].CoinsAmount;
        }
    }
}
