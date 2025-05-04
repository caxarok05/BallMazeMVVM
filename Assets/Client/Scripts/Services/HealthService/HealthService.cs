using Client.Scripts.Data;
using System;
using UnityEngine;
using UnityEngine.Timeline;
using Zenject;

namespace Client.Scripts.Services
{
    public class HealthService : IHealthService, IInitializable
    {
        public int HP { get; private set; }
        public int MaxHP { get; private set; }
        
        public event Action healthChanged;
        public event Action gameEnded;

        private IJsonDataService _dataService;
        private int currentLevel;

        public HealthService(IJsonDataService dataService, int level)
        {
            _dataService = dataService;
            currentLevel = level;
        }
        public void Initialize()
        {
            var config = _dataService.GameConfig;
            MaxHP = config.LevelData[currentLevel].HealthAmount;
            HP = MaxHP;
        }

        public void SpendHealth(int health)
        {
            if (HP - health > 0)
                HP -= health;
            else
            {
                HP = 0;
                Debug.Log("You can't spend more HP than you have. Your current HP is now 0");
                gameEnded?.Invoke();
            }
            healthChanged?.Invoke(); 
        }

        public void AddHealth(int health)
        {
            if (HP + health > MaxHP)
            {
                HP = MaxHP;
                Debug.Log("Add health: " + health + ", that cannot be added due to maxHp limit. Current Health is max now");
            }
            else if (HP + health < MaxHP)
                HP += health;
        }

        
    }
}
