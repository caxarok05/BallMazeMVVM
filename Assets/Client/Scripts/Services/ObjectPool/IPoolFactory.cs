using UnityEngine;

namespace Client.Scripts.Services
{
    public interface IPoolFactory<T> where T : MonoBehaviour
    {
        void CreateParent();
        T CreatePrefab();
    }
}

