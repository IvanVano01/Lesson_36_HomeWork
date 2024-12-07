using Assets.HomeWork.Develop.CommonServices.DataManagment;
using Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders;
using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using System.Collections;
using UnityEngine;

namespace Assets.HomeWork.Develop.MainMenu.Infrastructure
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        public IEnumerator Run(DIContainer container, MainMenuInputArgs mainMenuInputArgs)
        {
            _container = container;

            ProcessRegistrations();

            Debug.Log($"Подружаем ресурсы для сцены {mainMenuInputArgs}");            

            yield return new WaitForSeconds(1f);// симулируем ожидание
        }

        private void ProcessRegistrations()
        {
            // Делаем регистрации для сцены гейплэя
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))// для теста
            {
                _container.Resolve<SceneSwitcher>().ProcessSwitcherSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));
            }

            if(Input.GetKeyDown(KeyCode.S))
            {
                ISaveLoadService saveLoadService = _container.Resolve<ISaveLoadService>();

                if(saveLoadService.TryLoad(out PlayerData playerData ))// пробуем загрузить данные
                {
                    playerData.Money++;
                    playerData.CompletedLevels.Add(playerData.Money);

                    saveLoadService.Save(playerData);
                }
                else
                {
                    PlayerData originPlayerData = new PlayerData()// если данных ещё нет, то создаём данные по умолчанию
                    {
                        Money = 0,
                        CompletedLevels = new()
                    };

                    saveLoadService.Save(originPlayerData);
                }
            }
        }
    }
}