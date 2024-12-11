using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    public abstract class DataProvider<TData> where TData : ISaveData //абстрактный класс будет опрашивать все сервисы в которым нужно получать или сохранать данные
    {
        private readonly ISaveLoadService _saveLoadService;

        private List<IDataWriter<TData>> _writers = new(); // список где буду храниться сервисы оторые хотят сохранять данные
        private List<IDataReader<TData>> _readers = new(); // список где буду храниться сервисы оторые хотят считывать данные

        public DataProvider(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        private TData Data { get; set; } // св-во для получения данных

        public void RegisterWriter(IDataWriter<TData> writer)// метод регистрации сервисов которые хотят сохранять данные
        {
            if (_writers.Contains(writer))
                throw new ArgumentException(nameof(writer));

            _writers.Add(writer);
        }

        public void RegisterReader(IDataReader<TData> reader)// метод регистрации сервисов которые хотят считывать данные
        {
            if (_readers.Contains(reader))
                throw new ArgumentException(nameof(reader));

            _readers.Add(reader);
        }

        public void Load()// метод загрузки данных
        {
            if (_saveLoadService.TryLoad(out TData data))// если данные есть, то подгружаем их
                Data = data;
            else
                Reset();// запускаем метод резет, на тот случай если нет базовых данных(данные по умолчанию)

            foreach (IDataReader<TData> reader in _readers)// проходимся по списку сервисов, которые хотят считать данные, для считывания данных            
                reader.ReadFrom(Data);
        }

        public void Save()
        {
            foreach(IDataWriter<TData> writer in _writers)// проходимся по списку сервисов которые хотят сохранить данные
                writer.WriteTo(Data);// записываем данные в дату

            _saveLoadService.Save(Data);// сохраняем дату с обновлёнными данными
        }

        protected abstract TData GetOriginData();//получение данных по умолчанию, абстрактный метод,
                                                 //потому что у каждого сервиса который будет запрашивать данные, свои 
        private void Reset()
        {
            Data = GetOriginData();//получение данных по умолчанию
            Save(); // сохраняем данные
        }

    }
}
