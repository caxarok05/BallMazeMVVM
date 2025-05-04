using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Client.Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask<GameObject> Instantiate(string path, Vector3 at);
        UniTask<GameObject> Instantiate(string path);
        UniTask<GameObject> Instantiate(string path, Transform parent);
        UniTask<T> InstantiateComponent<T>(string path, Vector3 at);
        UniTask<T> InstantiateComponent<T>(string path);
        UniTask<T> InstantiateComponent<T>(string path, Transform parent);
    }
}