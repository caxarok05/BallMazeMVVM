namespace Client.Scripts.Services
{
    public interface IMoneyService
    {
        int Money { get; }
        int MoneyToNextLevel { get; }

        void AddMoney(int money);
    }
}