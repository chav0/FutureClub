using Project.Scripts.Presenters;

namespace Project.Scripts.Views
{
    public interface IUserInterfaceView
    {
        bool IsLeftPressed { get; }
        bool IsRightPressed { get; }
        bool IsPausePressed { get; }
        bool NewGame { get; }
        bool IsContinuePressed { get; }

        void Update(int coins, float progress, int score, int level, ApplicationState state);

        void ShowGameOver();
        void ShowNewGame();
        void ShowMainMenu();
        void ShowCharSelect();
        void ShowPause(); 
    }
}