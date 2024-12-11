using Assets.HomeWork.Develop.CommonServices.DI;
using UnityEngine;
using Assets.HomeWork.Develop.CommonServices.AssetManagment;
using Assets.HomeWork.Develop.CommonServices.CoroutinePerformer;
using Assets.HomeWork.Develop.CommonServices.LoadingScreen;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using System;
using Assets.HomeWork.Develop.CommonServices.DataManagment;
using Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders;
using Assets.HomeWork.Develop.CommonServices.Wallet;

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
            RegisterCoroutinePerformer(projectContainer);// регаем и подгружаем сервис запуска корутин

            RegisterLoadingCurtain(projectContainer);// регестрируем загрузочную шторку 
            RegisterSceneLoader(projectContainer);// регестрируем сервис для загрузки других сцен 
            RegisterSceneSwitcher(projectContainer);// регаем сервис для перехода в другие сцены

            RegisterSaveLoadService(projectContainer); // регаем сервис сохранений данных в формате Json
            RegisterPlayerDataProvider(projectContainer); //регаем дату, в которой будут храниться данные для игрока
            RegisterWalletService(projectContainer);// регаем сервис кошелька

            projectContainer.Initialize();// инициализируем контейнер, на предмет маркировки и создания объектов,
                                          // которые нужны до за ранее, до первого запроса их
            
            // все регистрации глобальные прошли

            projectContainer.Resolve<ICoroutinePerformer>().StartPerform(_gameBootstrap.Run(projectContainer));// из контеёнер достаём сервис старта
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
        }

        private void RegisterPlayerDataProvider(DIContainer сontainer)
         => сontainer.RegisterAsSingle<PlayerDataProvider>(c => new PlayerDataProvider(c.Resolve<ISaveLoadService>()));

        private void RegisterWalletService(DIContainer сontainer)
        => сontainer.RegisterAsSingle(c => new WalletService(c.Resolve<PlayerDataProvider>())).NonLazy();// сервис помечен как "NonLazy" должен создаться за ранее,
                                                                                                         // не дожидаясь первого запроса на него

        private void RegisterSaveLoadService(DIContainer container)
            => container.RegisterAsSingle<ISaveLoadService>(c => new SaveLoadService(new JsonSerializer(), new LocalDataRepository()));


        private void RegisterSceneSwitcher(DIContainer container)
        {
            container.RegisterAsSingle(c => new SceneSwitcher(
                c.Resolve<ICoroutinePerformer>(),
                c.Resolve<ILoadingCurtain>(),
                c.Resolve<ISeneLoader>(),
                c));
        }

        private void RegisterResourcesAssetLoader(DIContainer container)// метод регистрации подгрузки ресурсов из папки "Recources"
        {
            container.RegisterAsSingle<ResourcesAssetLoader>(c => new ResourcesAssetLoader());
        }

        private void RegisterCoroutinePerformer(DIContainer container)//метод регистрации подгрузки ресурсов из папки "Recources" сервиса запуска корутин
        {
            container.RegisterAsSingle<ICoroutinePerformer>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();// запрашиваем из контейнера "RecourcesAssetLoader"
                CoroutinePerformer coroutinePerformerPrefab = resourcesAssetLoader
                .LoadResource<CoroutinePerformer>(InfrastructurePath.CoroutinePerformerPath);// подгружаем из папки ресурсов префаб                            

                return Instantiate(coroutinePerformerPrefab);  // создаём префаб на сцене 
            });
        }

        private void RegisterLoadingCurtain(DIContainer container)// метод регистрации загрузочной шторки
        {
            container.RegisterAsSingle<ILoadingCurtain>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();
                StandardLoadingCurtain standardLoadingCurtainPrefab = resourcesAssetLoader
                .LoadResource<StandardLoadingCurtain>(InfrastructurePath.LoadingCurtain);

                return Instantiate(standardLoadingCurtainPrefab);
            });
        }

        private void RegisterSceneLoader(DIContainer container)// метод регистрации сервиса загрузки других сцен
        {
            container.RegisterAsSingle<ISeneLoader>(c => new DefaultSceneLoader());
        }
    }
}
