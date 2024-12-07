using Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders;
using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    public class SaveDataKeys
    {
        private static Dictionary<Type, string> Keys = new Dictionary<Type, string>()
        {
            {typeof(PlayerData), "PlayerData" }// сериализуем ключ т.е привязываем ктипу сохраняемых данных ключ
        };

        public static string GetKeyFor<TData>() where TData : ISaveData
            => Keys[typeof(TData)];
    }
}
