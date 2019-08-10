using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Objects.Game.Character
{
    public class Enemy : Character
    {
        public float DamageDelay;
        public int Damage;
        public float CoinProbability;
        public float SeedsProbability; 
        private Sequence _sequence;
        private bool _canDamage;
        private List<Health> _triggeredHealths = new List<Health>();
        
        public bool IsAttack { get; set; }
        public bool IsDead { get; set; }

        private void Update()
        {
            Attack();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            var player = col.gameObject.GetComponent<Player>();

            if (player != null)
                _triggeredHealths.Add(player.Health);

            var tower = col.gameObject.GetComponent<Tower>();

            if (tower != null)
                _triggeredHealths.Add(tower.Health);

            var fence = col.gameObject.GetComponent<Fence>();

            if (fence != null)
                _triggeredHealths.Add(fence.Health);
            
            var ridge = col.gameObject.GetComponent<Ridge>();

            if (ridge != null)
                _triggeredHealths.Add(ridge.Health);
            
            var house = col.gameObject.GetComponent<House>();

            if (house != null)
                _triggeredHealths.Add(house.Health);
            
            var potato = col.gameObject.GetComponent<Potato>();

            if (potato != null && !potato.IsCollisionEnemy)
            {
                Health.Damage(potato.Damage);
                potato.IsCollisionEnemy = true; 
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            var player = col.gameObject.GetComponent<Player>();

            if (player != null)
                _triggeredHealths.Remove(player.Health);

            var tower = col.gameObject.GetComponent<Tower>();

            if (tower != null)
                _triggeredHealths.Remove(tower.Health);

            var fence = col.gameObject.GetComponent<Fence>();

            if (fence != null)
                _triggeredHealths.Remove(fence.Health);
            
            var ridge = col.gameObject.GetComponent<Ridge>();

            if (ridge != null)
                _triggeredHealths.Remove(ridge.Health);
            
                        
            var house = col.gameObject.GetComponent<House>();

            if (house != null)
                _triggeredHealths.Remove(house.Health);
        }

        private void Attack()
        {
            Health health = null;

            if (_triggeredHealths.Count > 0)
            {
                health = _triggeredHealths[0]; 
            }
            
            if (_canDamage)
            {
                health?.Damage(Damage);
                _canDamage = false;
            }

            if (_sequence != null)
            {
                _canDamage = false;
                return;
            }

            if (health != null && health.CurrentHealth == 0)
                return;
            
            if (health == null)
                return;

            IsAttack = true;

            SetState("Attack");
            _sequence = DOTween.Sequence()
                .InsertCallback(DamageDelay, () => _canDamage = true)
                .AppendCallback(() =>
                {
                    _sequence.Kill();
                    _sequence = null;
                    IsAttack = false;
                });
        }

        public void Death()
        {
            SetState("Death");
            IsDead = true; 
            
            _sequence?.Kill(); 
            _sequence = DOTween.Sequence()
                .InsertCallback(2f, () => Destroy(gameObject))
                .AppendCallback(() =>
                {
                    _sequence.Kill();
                    _sequence = null;
                });
        }
    }
}
