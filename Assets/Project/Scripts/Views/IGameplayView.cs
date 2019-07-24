using Project.Scripts.Objects.Game.Character;
using Project.Scripts.Presenters;

namespace Project.Scripts.Views
{
    public interface IGameplayView
    {
        bool IsGameOver { get; }
        int Coins { get; }

        void Update(ApplicationState state);

        void SetDirectionOfPress(Direction direction, float multiplier);
        void ResetWorld();
        void SetPause(bool toTrue);
    }
}