using Client.Scripts.Data;
using Client.Scripts.Services;
using MVVM;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Client.Scripts.UI
{
    public class HealthViewModel : IInitializable, IDisposable
    {
        [Data("Health")]
        public readonly ReactiveProperty<string> Health = new();

        private readonly IHealthService _healthService;

        public HealthViewModel(IHealthService healthService)
        {
            _healthService = healthService;
        }
        public void Initialize()
        {
            Health.Value = _healthService.MaxHP.ToString();
            _healthService.healthChanged += OnHealthChanged;
        }
        public void Dispose()
        {
            _healthService.healthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            Health.Value = _healthService.HP.ToString();
        }

    }
}