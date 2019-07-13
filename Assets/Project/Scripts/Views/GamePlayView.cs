using System.Collections.Generic;
using Project.Scripts.Objects.Game;
using Project.Scripts.Presenters;
using UnityEngine;

namespace Project.Scripts.Views
{
    public class GamePlayView : IGameplayView
    {
        private Level _level;
        private Player _player;
        private List<Coin> _coins; 
        
        public bool IsGameOver { get; private set; }
        public int CoinsCollectedInLastGame { get; private set; }
        public int ScoreCollectedInLastGame { get; private set; }
        public float CurrentProgress => _level.CurrentProgress(); 

        public GamePlayView(Level level, Player player)
        {
            _level = level;
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
            _level.transform.Translate(10f * Vector3.down * Time.deltaTime);

            IsGameOver = _player.IsGameOver;

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

        public void SetDirectionOfPresss(Direction direction)
        {
            _player.Move(direction);
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

    public enum Direction
    {
        Left, 
        Right
    }
}
