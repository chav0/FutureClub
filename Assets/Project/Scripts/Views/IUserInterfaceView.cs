using Project.Scripts.Presenters;
using Project.Scripts.Views.UserInput;

namespace Project.Scripts.Views
{
    public interface IUserInterfaceView
    {
        UIMessage<int> IsLeftPressed { get; }
        UIMessage<int> IsRightPressed { get; }
        bool IsPausePressed { get; }
        bool NewGame { get; }
        bool IsContinuePressed { get; }

        void Update(int coins, int seeds, int food, int score, int level, ApplicationState state);

        void ShowGameOver();
        void ShowNewGame();
        void ShowMainMenu();
        void ShowCharSelect();
        void ShowPause(); 
    }
}