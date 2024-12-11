using Assets.HomeWork.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>// дата, в которой будут храниться данные для игрока 
    {
        // тут будем передавать сервис конфигов
        // и из кофигов вытягивать начальные данные
        public PlayerDataProvider(ISaveLoadService saveLoadService) : base(saveLoadService)
        {
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData()// если данных ещё нет, то создаём данные по умолчанию
            {
                WalletData = InitWalletData()
            };
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()// метод создания данных для валют
        {
            Dictionary<CurrencyTypes, int> walletData = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))// перебераем энамку с валютами
                walletData.Add(currencyType, 0);// добавляем валюты из энамки

            return walletData;
        }    
    }
}
