namespace Assets.HomeWork.Develop.CommonServices.SceneManagment
{
    public interface IInputSceneArgs// интерфейс пустой, просто для маркеровки
    {
    }

    // поскольку переходить мы можем только на сцену геймплэя и главного меню (сцена bootstrap загружается только один раз в начале),
    // создаём для передачи аргументов в эти сцены, отдельные классы

    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(int levelNumber)// конструктор
        {
            LevelNumber = levelNumber;
        }

        public int LevelNumber { get; }
    }

    public class MainMenuInputArgs : IInputSceneArgs
    {

    }
}
