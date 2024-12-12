using Assets.HomeWork.Develop.CommonServices.ConfigsManagment;
using Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders;
using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.LoadingScreen;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using System.Collections;
using UnityEngine;

namespace Assets.HomeWork.Develop.EntryPoint
{
    // EntryPoint - для глобальных регистраций которые стартуют проект
    // BootStrap - для инициализации начала работы
    public class Bootstrap : MonoBehaviour
    {
        public IEnumerator Run(DIContainer container)
        {
            //Включаем загрузочную штору после всех регистраций
            ILoadingCurtain loadingCurtain = container.Resolve<ILoadingCurtain>();
            SceneSwitcher sceneSwitcher = container.Resolve<SceneSwitcher>();

            loadingCurtain.Show();

            Debug.Log("Начинается инициализация сервисов в Bootstrap !");

            // Инициализация всех сервисов(конфиги, инит сервисы рекламы/ аналитики)

            container.Resolve<ConfigsProviderService>().LoadAll();// подгружаем конфиги

            container.Resolve<PlayerDataProvider>().Load();// загружаем начальные данные для игрока(из конфигов и т.д)

            yield return new WaitForSeconds(1.5f);// заглушка, имитирует инициализацию сервисов которые выше

            // скрываем штору
            loadingCurtain.Hide();

            Debug.Log("Завершается инициализация сервисов в Bootstrap, наченаем переход на другую сцену!");
            // переход на следующую сцену с помощью сервиса смены сцены

            sceneSwitcher.ProcessSwitcherSceneFor(new OutputBootstrapArgs(new MainMenuInputArgs()));
        }
    }
}
