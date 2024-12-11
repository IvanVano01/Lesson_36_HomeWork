namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    public interface IDataReader<TData> where TData : ISaveData // для всех сервисов которые хотят считывать данные при загрузки
    {
        void ReadFrom(TData data);// закидываем данные в метод и сервис их будет считывать
    }
}
