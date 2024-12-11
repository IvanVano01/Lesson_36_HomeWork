namespace Assets.HomeWork.Develop.CommonServices.SceneManagment
{
    public interface IOutputSceneArgs
    {
        IInputSceneArgs NextSceneInputArgs { get; }// выходные параметры сцены будут являться входными для той сцены в которую хотим перейти
        // т.е как пример, нельзя запустить сцену gameplay без указания какой уровень хотим загрузить
    }

    public abstract class OutputSeneArgs : IOutputSceneArgs
    {
        protected OutputSeneArgs(IInputSceneArgs nextSceneInputArgs)// конструктор
        {
            NextSceneInputArgs = nextSceneInputArgs;
        }

        public IInputSceneArgs NextSceneInputArgs {get;}
    }

    public class OutputGameplayArgs : OutputSeneArgs
    {
        public OutputGameplayArgs(IInputSceneArgs nextSceneInputArgs) : base(nextSceneInputArgs) // конструктор
        {
            // если надо то сюда пишем результат gameplay сцены который нужно передать в главное меню например
        }
    }

    public class OutputMainMenuArgs : OutputSeneArgs
    {
        public OutputMainMenuArgs(IInputSceneArgs nextSceneInputArgs) : base(nextSceneInputArgs)// конструктор
        {
        }
    }

    public class OutputBootstrapArgs : OutputSeneArgs
    {
        public OutputBootstrapArgs(IInputSceneArgs nextSceneInputArgs) : base(nextSceneInputArgs)
        {
        }
    }
}
