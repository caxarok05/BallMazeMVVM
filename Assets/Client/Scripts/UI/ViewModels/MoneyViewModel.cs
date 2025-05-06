using Client.Scripts.Infrastructure.Signals;
using Client.Scripts.Services;
using MVVM;
using System;
using UniRx;
using Zenject;

namespace Client.Scripts.UI
{
    public class MoneyViewModel : IInitializable, IDisposable
    {

        [Data("MoneyAmount")]
        public readonly ReactiveProperty<string> Money = new();

        private readonly IMoneyService _moneyService;
        private readonly SignalBus _signalBus;

        public MoneyViewModel(IMoneyService moneyService, SignalBus signalBus)
        {
            _moneyService = moneyService;
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            OnMoneyChanged();
            _signalBus.Subscribe<MoneyChanged>(OnMoneyChanged);
        }
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<MoneyChanged>(OnMoneyChanged);
        }

        private void OnMoneyChanged()
        {
            Money.Value = $"{_moneyService.Money}/{_moneyService.MoneyToNextLevel}";
        }

    }
}
