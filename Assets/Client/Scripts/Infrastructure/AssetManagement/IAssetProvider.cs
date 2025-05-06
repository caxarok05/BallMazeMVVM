using UnityEngine;

namespace Client.Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Transform parent);
        T InstantiateComponent<T>(string path, Vector3 at);
        T InstantiateComponent<T>(string path);
        T InstantiateComponent<T>(string path, Transform parent);
    }
}