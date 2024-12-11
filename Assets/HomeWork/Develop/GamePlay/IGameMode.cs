namespace Assets.HomeWork.Develop.GamePlay
{
    public interface IGameMode
    {
        bool IsGameOver { get;}
        bool IsWin { get; }

        void Update();
    }
}
