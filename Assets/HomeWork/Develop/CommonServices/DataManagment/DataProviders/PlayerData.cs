using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    [Serializable]

    internal class PlayerData : ISaveData
    {
        public int Money;
        public List<int> CompletedLevels;
    }
}
