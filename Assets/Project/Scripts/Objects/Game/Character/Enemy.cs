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
        private Sequence _sequence;
        private bool _canDamage;

        public bool IsAttack { get; set; }

        private void OnTriggerStay2D(Collider2D col)
        {
            var player = col.gameObject.GetComponent<Player>();

            if (player != null)
            {
                Attack(player.Health);
            } 

            var tower = col.gameObject.GetComponent<Tower>();
            
            if(tower != null)
                Attack(tower.Health); 
        }

        private void Attack(Health health)
        {
            if (_canDamage)
            {
                health.Damage(Damage);
                _canDamage = false;
            }

            if (_sequence != null)
            {
                _canDamage = false;
                return;
            }

            if (health.CurrentHealth == 0)
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
