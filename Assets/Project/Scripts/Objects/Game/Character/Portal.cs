using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Objects.Game.Character
{
    public class Portal : MonoBehaviour
    {
        public Enemy EnemyPrefab;
        public float TimeToSpawn;
        public List<int> SpawnDays;
        public int EnemySpawnsPerDay;

        public int TickSpawn { get; set; }
        
        public void Spawn()
        {
            for (var i = 0; i < EnemySpawnsPerDay; i++)
            {
                var enemy = Instantiate(EnemyPrefab);
                enemy.Transform.Transform.position =
                    new Vector3(transform.position.x + 15f * Random.value, -21f, 0f); 
            }
        }
    }
}
