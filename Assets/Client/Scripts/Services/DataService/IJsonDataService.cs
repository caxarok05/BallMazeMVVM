using Client.Scripts.Data;

namespace Client.Scripts.Services
{
    public interface IJsonDataService
    {
        T LoadData<T>();
    }
}