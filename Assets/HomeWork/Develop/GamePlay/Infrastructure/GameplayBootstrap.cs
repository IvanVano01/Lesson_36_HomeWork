using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("Сцена готова, можно начинать игру");

            yield return new WaitForSeconds(1f);// симулируем ожидание
        }

        private void ProcessRegistrations()
        {
            // Делаем регистрации для сцены гейплэя
        }

        private void Update()// для теста
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _container.Resolve<SceneSwitcher>().ProcessSwitcherSceneFor(new OutputGamplayArgs(new MainMenuInputArgs()));
            }
        }
    }
}