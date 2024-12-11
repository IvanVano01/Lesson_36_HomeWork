using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using System.Collections;
using UnityEngine;
using System;

namespace Assets.HomeWork.Develop.GamePlay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;

            ProcessRegistrations();

            Debug.Log($"Подружаем ресурсы для уровня {gameplayInputArgs.LevelNumber}");
            Debug.Log("Создаём персонажа");


            yield return new WaitForSeconds(1f);// симулируем ожидание

            Debug.Log("Сцена готова, можно начинать игру");

        }

        private void ProcessRegistrations()
        {
            // Делаем регистрации для сцены гейплэя

            _container.Initialize();// инициализируем контейнер, на предмет маркировки и создания объектов,
                                    // которые нужны до за ранее, до первого запроса их
        }

        private void Update()// для теста
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _container.Resolve<SceneSwitcher>().ProcessSwitcherSceneFor(new OutputGameplayArgs(new MainMenuInputArgs()));
            }

            
        }        
    }
}