namespace Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders
{
    public interface IDataWriter<TData> where TData : ISaveData // для всех сервисов которые хотят сохранять данные 
    {
        void WriteTo(TData data);// закидываем данные в метод и сервис их будет сщхранять
    }    
}
