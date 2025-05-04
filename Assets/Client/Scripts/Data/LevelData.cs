using System.Collections.Generic;

namespace Client.Scripts.Data
{
    public class LevelData
    {
        public int Level;

        public int HealthAmount;

        public int hedgehogAmount;
        public List<HedgehogData> HedgehogDatas = new List<HedgehogData>();

        public int CoinsAmount;
        public List<CoinData> CoinsDatas = new List<CoinData>();

        public PlayerPosition PlayerStartPositon;
        public PlayerPosition PlayerEndPositon;

    }
}