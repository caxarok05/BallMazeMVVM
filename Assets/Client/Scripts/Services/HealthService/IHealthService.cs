using System;

namespace Client.Scripts.Services
{
    public interface IHealthService
    {
        int HP { get; }
        int MaxHP { get; }

        event Action healthChanged;
        event Action gameEnded;

        void AddHealth(int health);
        void SpendHealth(int health);
    }
}