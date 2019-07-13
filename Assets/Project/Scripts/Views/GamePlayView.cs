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
        private List<Coin> _coins; 
        
        public bool IsGameOver { get; private set; }
        public int CoinsCollectedInLastGame { get; private set; }
        public int ScoreCollectedInLastGame { get; private set; }

        public GamePlayView(Player player)
        {
            _player = player; 
            _coins = new List<Coin>();
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

            for (var i = 0; i < _coins.Count; i++)
            {
                var coin = _coins[i];

                if (coin.IsCollisionEnter)
                {
                    CoinsCollectedInLastGame += coin.Value;
                    _coins.RemoveAt(i);
                    coin.Realise();
                }
            }
        }

        public void SetDirectionOfPress(Direction direction, float multiplier)
        {
            _player.Transform.Move(direction, multiplier);
        }

        public void ResetWorld()
        {
            IsGameOver = false;
            CoinsCollectedInLastGame = 0;
            ScoreCollectedInLastGame = 0; 
        }

        public void SetPause(bool toTrue)
        {

        }
    }
}
