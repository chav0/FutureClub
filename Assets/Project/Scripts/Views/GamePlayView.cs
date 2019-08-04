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
        private List<Enemy> _enemies; 
        private Level _level;
        private Settings _settings; 
        
        public bool IsGameOver { get; private set; }
        public int Coins { get; private set; }
        public int ScoreCollectedInLastGame { get; private set; }

        private readonly int _ticksPerDay; 
        
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
            _enemies = new List<Enemy>();
        }
        
        public void Update(ApplicationState state)
        {
            if (state == ApplicationState.Game)
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

                for (int i = _level.Places.Count - 1; i >= 0; i--)
                {
                    var place = _level.Places[i];

                    if (place.Destroy.TryGet())
                    {
                        Coins += place.ReturnCost;
                    }

                    if (place.SpendMoney.TryGet(out var coins))
                    {
                        Coins -= coins;
                    }

                    if (place.State == PlaceState.Tower)
                    {
                        
                    }
                }

                for (int i = _enemies.Count - 1; i >= 0; i--)
                {
                    var enemy = _enemies[i];

                    if (enemy.Health.IsDead)
                    {
                        enemy.Death();
                        _enemies.RemoveAt(i);
                        continue;
                    }
                    
                    if(!enemy.IsAttack)
                    {
                        var direction = enemy.Transform.Transform.position.x - _level.transform.position.x > 0
                            ? Direction.Left
                            : Direction.Right; 
                        enemy.Transform.Move(direction);
                    }
                }
            }
        }

        public void FixedUpdate(ApplicationState state)
        {
            if (state == ApplicationState.Game)
            {
                var day = (int) Math.Truncate(TimeHelper.CurrentTick / (float) _ticksPerDay);
                var tickOfDay = TimeHelper.CurrentTick % _ticksPerDay;

                foreach (var portal in _level.Portals)
                {
                    if (portal.TickSpawn == tickOfDay)
                    {
                        var enemies = portal.Spawn(day);
                        _enemies.AddRange(enemies);
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
