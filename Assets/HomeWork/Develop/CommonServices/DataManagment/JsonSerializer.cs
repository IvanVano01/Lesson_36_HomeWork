using Newtonsoft.Json;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    public class JsonSerializer : IDataSerializer
    {
        public TData Deserialize<TData>(string serializedData)
        {
           return JsonConvert.DeserializeObject<TData>(serializedData);// указываем тип и указываем строку, для десериализации
        }

        public string Serialize<TData>(TData data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            });
        }
    }
}
