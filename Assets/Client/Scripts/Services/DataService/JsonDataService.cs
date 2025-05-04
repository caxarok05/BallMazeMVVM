using Newtonsoft.Json;
using System.IO;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks;
using Client.Scripts.Data;
using Zenject;

namespace Client.Scripts.Services
{
    public class JsonDataService : IJsonDataService
    {
        private string JsonPath = Path.Combine(Application.streamingAssetsPath, "PlayerConfig.json");
        public GameConfig GameConfig {  get; private set; }

        public JsonDataService()
        {
            GameConfig = LoadData<GameConfig>();
        }

        public T LoadData<T>()
        {
            string path = JsonPath;

            if (!File.Exists(path))
            {
                Debug.LogError($"Cannot load file at {path}. File does not exist!");
                throw new FileNotFoundException($"{path} does not exist!");
            }

            try
            {
                T data;
                data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
                throw e;
            }
        }   
    }
}