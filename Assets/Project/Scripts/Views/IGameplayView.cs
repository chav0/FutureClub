using System.Collections.Generic;
using Project.Scripts.Objects.Game;
using Project.Scripts.Objects.Game.Character;
using Project.Scripts.Presenters;

namespace Project.Scripts.Views
{
    public interface IGameplayView
    {
        Player Player { get; }
        List<Enemy> Enemies { get; }
        Level Level { get; }
        bool IsGameOver { get; }
        int Coins { get; }
        int Seeds { get; }
        int Food { get; }

        void Update(ApplicationState state);
        void FixedUpdate(ApplicationState state);

        void SetDirectionOfPress(Direction direction, float multiplier);
        void ResetWorld();
        void SetPause(bool toTrue);
    }
}