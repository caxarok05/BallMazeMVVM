using UnityEngine;
using Zenject;

namespace Client.Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly DiContainer _container;
        public AssetProvider(DiContainer diContainer)
        {
            _container = diContainer;
        }
        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return _container.InstantiatePrefab(prefab, at, Quaternion.identity, null);
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return _container.InstantiatePrefab(prefab);
        }

        public GameObject Instantiate(string path, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return _container.InstantiatePrefab(prefab, parent);
        }

        public T InstantiateComponent<T>(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return _container.InstantiatePrefabForComponent<T>(prefab, at, Quaternion.identity, null);
        }

        public T InstantiateComponent<T>(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return _container.InstantiatePrefabForComponent<T>(prefab);
        }

        public T InstantiateComponent<T>(string path, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return _container.InstantiatePrefabForComponent<T>(prefab, parent);
        }
    }
}
