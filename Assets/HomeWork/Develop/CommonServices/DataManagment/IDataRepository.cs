namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    public interface IDataRepository
    {
        string Read(string key);// считываем файл данных(key- имя файла)

        void Write(string key, string serializedData);

        void Remove(string key);

        bool Exist(string key);
    }
}
