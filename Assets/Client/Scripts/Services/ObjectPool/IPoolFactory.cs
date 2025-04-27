using UnityEngine;

namespace Client.Scripts.Services
{
    public interface IPoolFactory<T> where T : Component
    {
        void CreateParent();
        T CreatePrefab();
    }
}

