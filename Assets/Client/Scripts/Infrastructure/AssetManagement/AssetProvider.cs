using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Client.Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        //is it normal using of unitask or too much

        private readonly DiContainer _container;
        public AssetProvider(DiContainer diContainer)
        {
            _container = diContainer;
        }
        public async UniTask<GameObject> Instantiate(string path, Vector3 at)
        {
            var prefab = await Resources.LoadAsync<GameObject>(path).ToUniTask();
            return _container.InstantiatePrefab(prefab, at, Quaternion.identity, null);
        }

        public async UniTask<GameObject> Instantiate(string path)
        {
            var prefab = await Resources.LoadAsync<GameObject>(path).ToUniTask();
            return _container.InstantiatePrefab(prefab);
        }

        public async UniTask<GameObject> Instantiate(string path, Transform parent)
        {
            var prefab = await Resources.LoadAsync<GameObject>(path).ToUniTask();
            return _container.InstantiatePrefab(prefab, parent);
        }

        public async UniTask<T> InstantiateComponent<T>(string path, Vector3 at)
        {
            var prefab = await Resources.LoadAsync<GameObject>(path).ToUniTask();
            return _container.InstantiatePrefabForComponent<T>(prefab, at, Quaternion.identity, null);
        }

        public async UniTask<T> InstantiateComponent<T>(string path)
        {
            var prefab = await Resources.LoadAsync<GameObject>(path).ToUniTask();
            return _container.InstantiatePrefabForComponent<T>(prefab);
        }

        public async UniTask<T> InstantiateComponent<T>(string path, Transform parent)
        {
            var prefab = await Resources.LoadAsync<GameObject>(path).ToUniTask();
            return _container.InstantiatePrefabForComponent<T>(prefab, parent);
        }
    }
}
