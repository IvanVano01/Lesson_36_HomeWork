using Assets.HomeWork.Develop.CommonServices.CoroutinePerformer;
using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.LoadingScreen;
using Assets.HomeWork.Develop.GamePlay.Infrastructure;
using Assets.HomeWork.Develop.MainMenu.Infrastructure;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.HomeWork.Develop.CommonServices.SceneManagment
{
    public class SceneSwitcher
    {
        private const string ErrorSceneTransitionMessage = " Данный переход не возможен";

        private readonly DIContainer _projectContainer;
        private readonly ICoroutinePerformer _coroutinePerformer;     // нам понадобится корутин перформер
        private readonly ILoadingCurtain _loadingCurtain;             // понадобиться загрузочная штора
        private readonly ISeneLoader _sceneLoader;

        private DIContainer _currentSceneContainer;// 

        public SceneSwitcher(
            ICoroutinePerformer coroutinePerformer,
            ILoadingCurtain loadingCurtain,
            ISeneLoader sceneLoader,
            DIContainer projectContainer)                                  // конструктор                                  
        {
            _coroutinePerformer = coroutinePerformer;
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _projectContainer = projectContainer;
        }

        public void ProcessSwitcherSceneFor(IOutputSceneArgs outputSceneArgs) // метод для перехода в другую сцену
        {
            switch (outputSceneArgs)// проходимся свитчерем что бы понять какие аргументы нам нужно передать в другую сцену
            {
                case OutputBootstrapArgs outputBootstrapArgs:// если это Bootstrap
                    _coroutinePerformer.StartPerform(ProcessSwitcherFromBootstrapScene(outputBootstrapArgs));
                    break;

                case OutputMainMenuArgs outputMainMenuArgs:// если это MainMenu
                    _coroutinePerformer.StartPerform(ProcessSwitcherFromMainMenuScene(outputMainMenuArgs));
                    break;

                case OutputGameplayArgs outputGamplayArgs: // если это сцена Gamplay
                    _coroutinePerformer.StartPerform(ProcessSwitcherFromGameplayScene(outputGamplayArgs));
                    break;

                default: throw new ArgumentException(nameof(outputSceneArgs));
            }
        }

        private IEnumerator ProcessSwitcherFromBootstrapScene(OutputBootstrapArgs outputBootstrapArgs)
        {
            switch (outputBootstrapArgs.NextSceneInputArgs) // определяем варианты перехода из сцены bootstrap
            {
                case MainMenuInputArgs mainMenuInputArgs: // из бутстрапа можем перейти в сцену главного меню
                    yield return ProcessSwitchToMainMenuScene(mainMenuInputArgs);
                    break;                

                default:
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }

        private IEnumerator ProcessSwitcherFromMainMenuScene(OutputMainMenuArgs outputMainMenuArgs)
        {
            switch (outputMainMenuArgs.NextSceneInputArgs) // определяем варианты перехода из сцены MainMenu
            {
                case GameplayInputArgs gameplayInputArgs: // из главного меню можем перейти в сцену геймплэя
                    yield return ProcessSwitchToGameplayScene(gameplayInputArgs);
                    break;                

                default:
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }

        private IEnumerator ProcessSwitcherFromGameplayScene(OutputGameplayArgs outputGamplayArgs)
        {
            switch (outputGamplayArgs.NextSceneInputArgs) // определяем варианты перехода из сцены Gameplay
            {
                case MainMenuInputArgs mainMenuInputArgs: //из геймплэя можем перейти только в главное меню
                    yield return ProcessSwitchToMainMenuScene(mainMenuInputArgs);
                    break;

                //--------------------------------------------------------------------------------------------//
                case GameplayInputArgs gameplayInputArgs: //из геймплэя  в геймплэй
                    yield return ProcessSwitchToGameplayScene(gameplayInputArgs);
                    break;
                //--------------------------------------------------------------------------------------------//

                default:
                    throw new ArgumentException(ErrorSceneTransitionMessage);
            }
        }

        private IEnumerator ProcessSwitchToMainMenuScene(MainMenuInputArgs mainMenuInputArgs)// для перехода на сцену главного меню
        {
            _loadingCurtain.Show();// показываем штору

            _currentSceneContainer?.Dispose();// при переходе на другую сцену вызываем метод отписки от всяких подписок

            yield return _sceneLoader.LoadAsync(SceneID.Empty);// сначала переходим на пустую сцену
            yield return _sceneLoader.LoadAsync(SceneID.MainMenu);// переходим на сцену главного меню

            MainMenuBootstrap mainMenuBootstrap =  UnityEngine.Object.FindAnyObjectByType<MainMenuBootstrap>();// ищем объект с компонентом "MainMenuBootstrap"

            if (mainMenuBootstrap == null)
                throw new NullReferenceException(nameof(mainMenuBootstrap));

            _currentSceneContainer = new DIContainer(_projectContainer);// создаём контейнер для сцены и внего вкладываем ссылку на родительский
            yield return mainMenuBootstrap.Run(_currentSceneContainer, mainMenuInputArgs);

            _loadingCurtain.Hide();
        }

        private IEnumerator ProcessSwitchToGameplayScene(GameplayInputArgs gameplayInputArgs)// для перехода на сцену геймплэя
        {
            _loadingCurtain.Show();

            _currentSceneContainer?.Dispose();// при переходе на другую сцену вызываем метод отписки от всяких подписок

            yield return _sceneLoader.LoadAsync(SceneID.Empty);// сначала переходим на пустую сцену
            yield return _sceneLoader.LoadAsync(SceneID.GamePlay);// переходим на сцену геймплэя

            GameplayBootstrap gameplayBootstrap  = UnityEngine.Object.FindAnyObjectByType<GameplayBootstrap>();

            if (gameplayBootstrap == null)
                throw new NullReferenceException(nameof(gameplayBootstrap));

            _currentSceneContainer = new DIContainer(_projectContainer);// создаём контейнер для сцены и внего вкладываем ссылку на родительский
            yield return gameplayBootstrap.Run(_currentSceneContainer, gameplayInputArgs);

            _loadingCurtain.Hide();
        }
    }
}
