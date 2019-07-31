using System;
using System.Collections.Generic;
using Project.Scripts.Models;
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
        private Settings _settings; 
        
        public bool IsGameOver { get; private set; }
        public int Coins { get; private set; }
        public int ScoreCollectedInLastGame { get; private set; }

        private int _ticksPerDay; 
        
        public GamePlayView(Player player, Level level, Settings settings)
        {
            _player = player;
            _level = level;
            _settings = settings;

            foreach (var portal in _level.Portals)
            {
                portal.TickSpawn = (int) Math.Truncate(portal.TimeToSpawn * _settings.DayDuration / Time.fixedDeltaTime);
            }

            _ticksPerDay = (int) Math.Truncate(settings.DayDuration / Time.fixedDeltaTime); 
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
            var day = (int) Math.Truncate(TimeHelper.CurrentTick / (float) _ticksPerDay);
            var tickOfDay = TimeHelper.CurrentTick % _ticksPerDay; 

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

            foreach (var tower in _level.Towers)
            {
                if (tower.Build.TryGet())
                {
                    if (tower.Cost <= Coins && tower.State == TowerState.Ruin)
                    {
                        tower.ChangeState(TowerState.Building);
                        Coins -= tower.Cost; 
                    }
                }
            }

            foreach (var portal in _level.Portals)
            {
                if (portal.SpawnDays.Contains(day))
                {
                    if (portal.TickSpawn == tickOfDay)
                    {
                        portal.Spawn();
                    }
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
