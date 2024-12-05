using Assets.HomeWork.Develop.CommonServices.DI;
using UnityEngine;
using Assets.HomeWork.Develop.CommonServices.AssetManagment;
using Assets.HomeWork.Develop.CommonServices.CoroutinePerformer;


namespace Assets.HomeWork.Develop.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Bootstrap _gameBootstrap;

        private void Awake()
        {
            SetupAppSettings();

            DIContainer projectContainer = new DIContainer();

            //регистрация сервисов на весь проект
            // global context
            // это родительский контейнер

            RegisterResourcesAssetLoader(projectContainer);// регестрируем подгрузку из папки ресурсов, "с" - контейнер
            RegidterCoroutinePerformer(projectContainer);// регаем и подгружаем сервис запуска корутин

            // все регистрации глобальные прошли
             
            projectContainer.Resolve<ICoroutinePerformer>().StartPerform(_gameBootstrap.Run(projectContainer));// из контеёнер достаём сервис старта
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
        }

        private void RegisterResourcesAssetLoader(DIContainer container)// метод регистрации подгрузки ресурсов из папки "Recources"
        {
            container.RegisterAsSingle<ResourcesAssetLoader>(c => new ResourcesAssetLoader());
        }

        private void RegidterCoroutinePerformer(DIContainer container)//метод регистрации подгрузки ресурсов из папки "Recources" сервиса запуска корутин
        {
            container.RegisterAsSingle<ICoroutinePerformer>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();// запрашиваем из контейнера "RecourcesAssetLoader"
                CoroutinePerformer coroutinePerformerPrefab = resourcesAssetLoader.LoadResource<CoroutinePerformer>("Infrastructure/CoroutinePerformer");// подгружаем из папки ресурсов префаб                            

                return Instantiate(coroutinePerformerPrefab);  // создаём префаб на сцене 
            });
        }
    }
}
