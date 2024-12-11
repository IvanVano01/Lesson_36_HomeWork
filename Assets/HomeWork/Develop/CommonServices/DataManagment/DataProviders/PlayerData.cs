using Assets.HomeWork.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    [Serializable]

    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;// словарь для хранения валют
    }
}
