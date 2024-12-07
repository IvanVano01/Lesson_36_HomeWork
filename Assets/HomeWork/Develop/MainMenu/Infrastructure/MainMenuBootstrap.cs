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

        private void Update()// для теста
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _container.Resolve<SceneSwitcher>().ProcessSwitcherSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));
            }
        }
    }
}