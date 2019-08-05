using System;
using System.Collections.Generic;
using Project.Scripts.Models;
using Project.Scripts.Objects.Game;
using Project.Scripts.Objects.Game.Character;
using Project.Scripts.Presenters;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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
        public int Seeds { get; private set; }
        public int Food { get; private set; }
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

                if (_player.EatFood.TryGet())
                {
                    if (Food > 0)
                    {
                        _player.Eat();
                        Food--; 
                    }
                }

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
                
                for (var i = 0; i < _level.Seeds.Count; i++)
                {
                    var seeds = _level.Seeds[i];

                    if (seeds.IsCollisionEnter)
                    {
                        Seeds += seeds.Value;
                        _level.Seeds.RemoveAt(i);
                        seeds.Realise();
                    }
                }

                for (int i = _level.Places.Count - 1; i >= 0; i--)
                {
                    var place = _level.Places[i];

                    if (place.Destroy.TryGet())
                    {
                        Coins += place.ReturnCost;
                        _player.Build(); 
                    }

                    if (place.State == PlaceState.Empty)
                    {
                        if (place.BuyTower.TryGet(out var coins))
                        {
                            if (Coins >= coins && _player.Energy.CurrentEnergy >= _player._spendEnergyForBuilding)
                            {
                                Coins -= coins;
                                place.SetTower();
                                _player.Build(); 
                            }
                        }
                        
                        if (place.BuyFence.TryGet(out coins))
                        {
                            if (Coins >= coins && _player.Energy.CurrentEnergy >= _player._spendEnergyForBuilding)
                            {
                                place.SetFence();
                                Coins -= coins;
                                _player.Build(); 
                            }
                        }
                        
                        if (place.BuyRidge.TryGet(out coins))
                        {
                            if (Coins >= coins && _player.Energy.CurrentEnergy >= _player._spendEnergyForBuilding)
                            {
                                place.SetRidge();
                                Coins -= coins;
                                _player.Build(); 
                            }
                        }
                    }

                    if (place.State == PlaceState.Tower)
                    {
                        if (place.Tower.Health.CurrentHealth == 0)
                        {
                            place.SetEmpty();
                        }

                        var nearestEnemyPosition = new Vector3();
                        var nearestRange = place.Tower.Range;
                        var attack = false; 
                        
                        foreach (var enemy in _enemies)
                        {
                            var distance = Math.Abs(enemy.Transform.Position.x - place.Tower.transform.position.x);
                            
                            if (distance < place.Tower.Range && distance < nearestRange)
                            {
                                attack = true;
                                nearestRange = distance;
                                nearestEnemyPosition = enemy.Transform.Position; 
                            }
                        }

                        if (attack)
                        {
                            place.Tower.Attack(nearestEnemyPosition);
                        }
                    }
                    
                    if (place.State == PlaceState.Fence)
                    {
                        if (place.Fence.Health.CurrentHealth == 0)
                        {
                            place.SetEmpty();
                        }
                    }

                    if (place.State == PlaceState.Ridge)
                    {
                        if (place.Ridge.Health.CurrentHealth == 0)
                        {
                            place.SetEmpty();
                        }

                        if (place.SpendSeeds.TryGet(out var seeds))
                        {
                            if (Seeds > 0)
                            {
                                Seeds--;
                                place.Ridge.Plant();
                            } 
                        }

                        if (place.Loot.TryGet(out var food))
                        {
                            Food += food; 
                        }
                    }

                    if (place.Repair.TryGet(out var cost, out var building))
                    {
                        if (Coins >= cost && _player.Energy.CurrentEnergy >= _player._spendEnergyForBuilding)
                        {
                            Coins -= cost;
                            switch (building)
                            {
                                case PlaceState.Tower:
                                    place.Tower.Health.RestoreHealth();
                                    break;
                                case PlaceState.Fence:
                                    place.Fence.Health.RestoreHealth();
                                    break;
                                case PlaceState.Ridge:
                                    place.Ridge.Health.RestoreHealth();
                                    break;
                            }
                            _player.Build(); 
                        } 
                    }
                }

                for (int i = _enemies.Count - 1; i >= 0; i--)
                {
                    var enemy = _enemies[i];

                    if (enemy.Health.IsDead && !enemy.IsDead)
                    {
                        enemy.Death();
                        RandomEnemyLoot(enemy); 
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

        private void RandomEnemyLoot(Enemy enemy)
        {
            var random = Random.value;

            if (random < enemy.CoinProbability)
            {
                var coin = Object.Instantiate(_level.CoinPrefab);
                coin.transform.position = enemy.transform.position + new Vector3(Random.value * 10f, 0f, 0f); 
                _level.Coins.Add(coin);
            }

            random = Random.value;

            if (random < enemy.SeedsProbability)
            {
                var seeds = Object.Instantiate(_level.SeedsPrefab);
                seeds.transform.position = enemy.transform.position + new Vector3(Random.value * 10f, 0f, 0f); 
                _level.Seeds.Add(seeds);
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
            _player.Move(direction, multiplier);
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
