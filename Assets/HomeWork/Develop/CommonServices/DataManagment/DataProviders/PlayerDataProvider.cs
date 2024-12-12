using Assets.HomeWork.Develop.CommonServices.ConfigsManagment;
using Assets.HomeWork.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>// дата, в которой будут храниться данные для игрока 
    {
        // тут будем передавать сервис конфигов
        // и из кофигов вытягивать начальные данные
        private ConfigsProviderService _configProviderService;// поле для провайдера конфигов

        public PlayerDataProvider(ISaveLoadService saveLoadService, ConfigsProviderService configsProviderService) : base(saveLoadService)
        {
            _configProviderService = configsProviderService;
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
                walletData.Add(currencyType, _configProviderService.StartWalletConfig.GetStartValueFor(currencyType));// добавляем валюты из конфига

            return walletData;
        }    
    }
}
