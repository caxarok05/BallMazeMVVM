using System.Collections.Generic;
using UnityEngine;

namespace Client.Scripts.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string ChefsDataPath = "Static Data/Chef";

        // private Dictionary<int, ChefStaticData> _chefs;

        public void Load()
        {
            //_chefs = Resources
            //  .LoadAll<ChefStaticData>(ChefsDataPath)
            //  .ToDictionary(x => x.UpgradeLevel, x => x);
        }

        //public ChefStaticData ForChefs(int upgradeLevel) =>
        //  _chefs.TryGetValue(upgradeLevel, out ChefStaticData staticData)
        //    ? staticData
        //    : null;

    }

}