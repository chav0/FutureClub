using System.Collections.Generic;
using Project.Scripts.Objects.Game;
using Project.Scripts.Objects.Game.Character;
using Project.Scripts.Presenters;
using UnityEngine;

namespace Project.Scripts.Views
{
    public class GamePlayView : IGameplayView
    {
        private Player _player;
        private Level _level; 
        
        public bool IsGameOver { get; private set; }
        public int Coins { get; private set; }
        public int ScoreCollectedInLastGame { get; private set; }

        public GamePlayView(Player player, Level level)
        {
            _player = player;
            _level = level; 
        }
        
        public void Update(ApplicationState state)
        {
            if (state == ApplicationState.Game)
            {
                GameUpdate();
            }
        }

        private void GameUpdate()
        {
            IsGameOver = _player.Health.IsDead;

            for (var i = 0; i < _level.Coins.Count; i++)
            {
                var coin = _level.Coins[i];

                if (coin.IsCollisionEnter)
                {
                    Coins += coin.Value;
                    _level.Coins.RemoveAt(i);
                    coin.Realise();
                }
            }
        }

        public void SetDirectionOfPress(Direction direction, float multiplier)
        {
            _player.Transform.Move(direction, multiplier);
            _level.Move(_player.Transform.Position);
        }

        public void ResetWorld()
        {
            IsGameOver = false;
            Coins = 0;
            ScoreCollectedInLastGame = 0; 
        }

        public void SetPause(bool toTrue)
        {

        }
    }
}
