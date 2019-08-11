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
        public Player Player { get; private set; }
        public List<Enemy> Enemies { get; private set; }
        public Level Level { get; private set; }
        private Settings _settings;
        private House _house;
        
        private int dayListNum = 0;
        
        public bool IsGameOver { get; private set; }
        public bool IsVictory { get; private set; }
        public int Coins { get; private set; }
        public int Seeds { get; private set; }
        public int Food { get; private set; }
        public int ScoreCollectedInLastGame { get; private set; }

        private readonly int _ticksPerDay; 
        
        public GamePlayView(Player player, Level level, Settings settings, House house)
        {
            Player = player;
            Level = level;
            _settings = settings;
            _house = house;

            foreach (var portal in Level.Portals)
            {
                portal.TickSpawn = (int) Math.Truncate(portal.TimeToSpawn * _settings.DayDuration / Time.fixedDeltaTime);
            }
            _ticksPerDay = (int) Math.Truncate(settings.DayDuration / Time.fixedDeltaTime); 
            Enemies = new List<Enemy>();
        }
        
        public void Update(ApplicationState state)
        {
            if (state == ApplicationState.Game || state == ApplicationState.UnlimitedGame)
            {
                IsGameOver = Player.Health.IsDead || _house.Health.IsDead;

                if (Player.EatFood.TryGet())
                {
                    if (Food > 0)
                    {
                        Player.Eat();
                        Food--; 
                    }
                }

                if (_house.Sell.TryGet(out var foodMinus))
                {
                    if (Food >= _house._foodForSellCount)
                    {
                        Food -= foodMinus;
                        Coins += _house._foodCost;
                    }
                }
                
                if (_house.HouseUp.TryGet(out var coinsMinus))
                {
                    if (Coins >= _house._houseUpCost)
                    {
                        Coins -= coinsMinus;
                        _house.HouseSprite.sprite = _house.GoodHouseSprite;
                        IsVictory = true; 
                    }
                }

                for (var i = 0; i < Level.Coins.Count; i++)
                {
                    var coin = Level.Coins[i];

                    if (coin.IsCollisionEnter)
                    {
                        Coins += coin.Value;
                        Level.Coins.RemoveAt(i);
                        coin.Realise();
                    }
                }
                
                for (var i = 0; i < Level.Seeds.Count; i++)
                {
                    var seeds = Level.Seeds[i];

                    if (seeds.IsCollisionEnter)
                    {
                        Seeds += seeds.Value;
                        Level.Seeds.RemoveAt(i);
                        seeds.Realise();
                    }
                }

                for (int i = Level.Places.Count - 1; i >= 0; i--)
                {
                    var place = Level.Places[i];

                    if (place.Destroy.TryGet())
                    {
                        Coins += place.ReturnCost;
                        Player.Build(); 
                    }

                    if (place.State == PlaceState.Empty)
                    {
                        if (place.BuyTower.TryGet(out var coins))
                        {
                            if (Coins >= coins && Player.Energy.CurrentEnergy >= Player._spendEnergyForBuilding)
                            {
                                Coins -= coins;
                                place.SetTower();
                                Player.Build(); 
                            }
                            else if (Player.Energy.CurrentEnergy < Player._spendEnergyForBuilding)
                            {
                                Player.ShowLowEnergy();
                            }
                        }
                        
                        if (place.BuyFence.TryGet(out coins))
                        {
                            if (Coins >= coins && Player.Energy.CurrentEnergy >= Player._spendEnergyForBuilding)
                            {
                                place.SetFence();
                                Coins -= coins;
                                Player.Build(); 
                            }
                            else if (Player.Energy.CurrentEnergy < Player._spendEnergyForBuilding)
                            {
                                Player.ShowLowEnergy();
                            }
                        }
                        
                        if (place.BuyRidge.TryGet(out coins))
                        {
                            if (Coins >= coins && Player.Energy.CurrentEnergy >= Player._spendEnergyForBuilding)
                            {
                                place.SetRidge();
                                Coins -= coins;
                                Player.Build(); 
                            }
                            else if (Player.Energy.CurrentEnergy < Player._spendEnergyForBuilding)
                            {
                                Player.ShowLowEnergy();
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
                        
                        foreach (var enemy in Enemies)
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
                            place.Ridge.SetEmpty();
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
                        if (Coins >= cost && Player.Energy.CurrentEnergy >= Player._spendEnergyForBuilding)
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
                            Player.Build(); 
                        }
                        else if (Player.Energy.CurrentEnergy < Player._spendEnergyForBuilding)
                        {
                            Player.ShowLowEnergy();
                        }
                    }
                }

                for (int i = Enemies.Count - 1; i >= 0; i--)
                {
                    var enemy = Enemies[i];

                    if (enemy.Health.IsDead && !enemy.IsDead)
                    {
                        enemy.Death();
                        RandomEnemyLoot(enemy); 
                        Enemies.RemoveAt(i);
                        continue;
                    }
                    
                    if(!enemy.IsAttack)
                    {
                        var direction = enemy.Transform.Transform.position.x - Level.MainBuilding.position.x > 0
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
                var coin = Object.Instantiate(Level.CoinPrefab);
                coin.transform.position = enemy.transform.position + new Vector3(Random.value * 10f, 0f, 0f); 
                Level.Coins.Add(coin);
            }

            random = Random.value;

            if (random < enemy.SeedsProbability)
            {
                var seeds = Object.Instantiate(Level.SeedsPrefab);
                seeds.transform.position = enemy.transform.position + new Vector3(Random.value * 10f, 0f, 0f); 
                Level.Seeds.Add(seeds);
            }
        }

        public void FixedUpdate(ApplicationState state)
        {
            if (state == ApplicationState.Game || state == ApplicationState.UnlimitedGame)
            {
                var day = (int) Math.Truncate(TimeHelper.CurrentTick / (float) _ticksPerDay);
                var tickOfDay = TimeHelper.CurrentTick % _ticksPerDay;

                foreach (var portal in Level.Portals)
                {
                    if ((tickOfDay > 0) && (tickOfDay % portal.TickSpawn == 0))
                    {
                        var enemies = portal.Spawn(dayListNum);
                        dayListNum++;
                        Enemies.AddRange(enemies);
                    }
                }
            }
        }

        public void SetDirectionOfPress(Direction direction, float multiplier)
        {
            Player.Move(direction, multiplier);
            Level.Move(Player.Transform.Position);
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
