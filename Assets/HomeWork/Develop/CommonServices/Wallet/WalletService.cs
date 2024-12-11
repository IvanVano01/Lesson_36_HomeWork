using Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders;
using Assets.HomeWork.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.HomeWork.Develop.CommonServices.Wallet
{
    public class WalletService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private Dictionary<CurrencyTypes, ReactiveVariable<int>> _currencies = new();// словарь ждя хранения валют

        public WalletService(PlayerDataProvider playerDataProvider)// конструктор
        {
           playerDataProvider.RegisterWriter(this); // регистрируем провайдер записи данных
           playerDataProvider.RegisterReader(this); // регистрируем провайдер считывания данных
        }

        public List<CurrencyTypes> AvaibleCurrencies => _currencies.Keys.ToList(); // св-во для получения какие валюты доступны в кошельке

        public IReadOnlyVariable<int> GetCurrency(CurrencyTypes type) => _currencies[type];// считываем через интерфейс "IReadOnlyVariable"
                                                                                           // значение валюты, которую хотим порлучить

        public bool HasEnough(CurrencyTypes type, int amount) => _currencies[type].Value >= amount;// проверяем хватает ли кол-ва валюты в кошельке


        public void Spend(CurrencyTypes type, int amount)// тратим валюту
        {
            if (HasEnough(type, amount) == false)
                throw new ArgumentException(type.ToString());

            _currencies[type].Value -= amount;
        }

        public void Add(CurrencyTypes type, int amount)// добавляем
        {
            _currencies[type].Value += amount;
        }

        public void ReadFrom(PlayerData data)// считываем данные, реализуем метод интерфейса "IDataReader"
        {
            foreach (KeyValuePair<CurrencyTypes, int> currency in data.WalletData) // проходимся по словорю валют и считываем количество денег
            {
                if (_currencies.ContainsKey(currency.Key))            // если кошелёк уже содержит такую валюту, то считываем её кол-во
                    _currencies[currency.Key].Value = currency.Value;

                else                                                 // если в кошелёке нет такой валюты, то добавляем её и считываем её кол-во
                    _currencies.Add(currency.Key, new ReactiveVariable<int>(currency.Value));
            }
        }

        public void WriteTo(PlayerData data)//записываем данные, реализуем метод интерфейса "IDataWriter"
        {
            foreach (KeyValuePair<CurrencyTypes, ReactiveVariable<int>> currency in _currencies)
            {
                if (data.WalletData.ContainsKey(currency.Key))          // если кошелёк уже содержит такую валюту, записываем её кол-во
                    data.WalletData[currency.Key] = currency.Value.Value;
                else                                                    // если в кошелёке нет такой валюты, то добавляем её и записываем её кол-во
                    data.WalletData.Add(currency.Key, currency.Value.Value);
            }
        }
    }
}
