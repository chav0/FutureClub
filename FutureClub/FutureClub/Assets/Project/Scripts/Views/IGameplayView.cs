using Project.Scripts.Presenters;

namespace Project.Scripts.Views
{
    public interface IGameplayView
    {
        bool IsGameOver { get; }
        int CoinsCollectedInLastGame { get; }
        int ScoreCollectedInLastGame { get; }
        float CurrentProgress { get; }

        void Update(ApplicationState state);

        void SetDirectionOfPresss(Direction direction);
        void ResetWorld();
        void SetPause(bool toTrue);
    }
}