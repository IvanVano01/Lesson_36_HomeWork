namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IDataSerializer _serializer;
        private readonly IDataRepository _repository;

        public SaveLoadService(IDataSerializer serializer, IDataRepository repository)
        {
            _serializer = serializer;
            _repository = repository;
        }

        public void Save<TData>(TData data) where TData : ISaveData // метод сохранения данных
        {
            string serializeData = _serializer.Serialize(data);// среализуем переданные данные
            _repository.Write(SaveDataKeys.GetKeyFor<TData>(), serializeData);// записываем сереализованные данные в репозиторий по ключу
        }

        public bool TryLoad<TData>(out TData data) where TData : ISaveData // метод получения сохранённых данных
        {
            string key = SaveDataKeys.GetKeyFor<TData>();// получаем ключ

            if(_repository.Exist(key) ==false)// проверяем существуют ли данные по полученному ключу
            {
                data = default(TData);// устанавливаем дефолтные значения
                return false;
            }

            string serializedData = _repository.Read(key);// получаем данные из репозетория по ключу
            data = _serializer.Deserialize<TData>(serializedData);// десериализуем полученные данные 

            return true;
        }
    }
}
