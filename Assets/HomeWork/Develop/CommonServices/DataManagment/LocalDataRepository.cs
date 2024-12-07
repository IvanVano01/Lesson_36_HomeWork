using System.IO;
using UnityEngine;

namespace Assets.HomeWork.Develop.CommonServices.DataManagment
{
    public class LocalDataRepository : IDataRepository
    {
        private const string SaveFileExtension = "json";// расширения сохранённого файла

        private string FolderPath => Application.persistentDataPath;// куда сохранять, юнити определит автоматически

        public bool Exist(string key) => File.Exists(FullPathFor(key));// проверяем, есть ли файл по этому пути
        

        public string Read(string key) => File.ReadAllText(FullPathFor(key));// считываем 
        

        public void Remove(string key) =>File.Delete(FullPathFor(key));
        

        public void Write(string key, string serializedData) // записываем
            => File.WriteAllText(FullPathFor(key), serializedData);
        

        private string FullPathFor(string key)// метод будет формировать полный путь до файла по ключу
        {
            return Path.Combine(FolderPath, key) + "." + SaveFileExtension;
        }
    }
}
