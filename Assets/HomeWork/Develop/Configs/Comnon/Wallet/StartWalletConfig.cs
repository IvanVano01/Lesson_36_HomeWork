using Assets.HomeWork.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.HomeWork.Develop.Configs.Comnon.Wallet
{
    [CreateAssetMenu(menuName = "Config/Wallet/NewStartWalletConfig", fileName = "StartWalletConfig")]

    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;// список, сколько в каждой валюте есть денег

        public int GetStartValueFor(CurrencyTypes currencyType) 
            => _values.First(config => config.Type == currencyType).Value;// находим из всех конфигов тот конфиг,
                                                                          // который удовлетворяет запрашиваему типу
                                                                          // и возвращаем значение этой валюты

        private void OnValidate()// метод для праверки, для все ди валют мы создали файлики ScriptableObject/конфиги
                                 // с учётом энамки"CurrencyTypes"
        {
            // нет ли дубликатов конфигов для валют и т.д
        }

        [Serializable]
        private class CurrencyConfig// класс для отображение значение валют
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }

            [field: SerializeField] public int Value { get; private set; }
        }
    }
}
