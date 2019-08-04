using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Objects.Game.Character
{
    public class Portal : MonoBehaviour
    {
        public Enemy EnemyPrefab;
        public float TimeToSpawn;
        public List<int> SpawnPerDay;

        public int TickSpawn { get; set; }
        
        public List<Enemy> Spawn(int day)
        {
            var enemySpawnsPerDay = SpawnPerDay[day]; 
            var enemies = new List<Enemy>();
            for (var i = 0; i < enemySpawnsPerDay; i++)
            {
                var enemy = Instantiate(EnemyPrefab);
                enemy.Transform.Transform.position =
                    new Vector3(transform.position.x + 15f * Random.value, -21f, 0f); 
                enemies.Add(enemy);
            }

            return enemies; 
        }
    }
}
