using Client.Scripts.Data;
using Cysharp.Threading.Tasks;

namespace Client.Scripts.Services
{
    public interface IJsonDataService
    {
        GameConfig GameConfig { get; }
        T LoadData<T>();
    }
}