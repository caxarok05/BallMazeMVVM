using UnityEngine;

namespace Client.Scripts.Services
{
    public class PoolFactory<T> : IPoolFactory<T> where T : Component
    {
        private GameObject _parent;
        private readonly T _prefab;
        public PoolFactory(GameObject parent, T prefab)
        {
            _parent = parent;
            _prefab = prefab;

        }

        public T CreatePrefab()
        {
            return Object.Instantiate(_prefab, _parent.transform);
        }

        public void CreateParent()
        {
            _parent = Object.Instantiate(_parent);
        }
    }
}

