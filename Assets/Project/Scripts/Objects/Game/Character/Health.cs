using System.Collections.Generic;
using Anima2D;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Objects.Game.Character
{
    public class Health 
    {
        public int CurrentHealth;
        public int MaxHealth;
        public bool IsDead => CurrentHealth <= 0;

        private List<SpriteMeshInstance> _meshes;
        private SpriteRenderer _spriteRenderer;
        private Sequence _sequence; 

        public Health(int health, List<SpriteMeshInstance> meshes = null, SpriteRenderer spriteRenderer = null)
        {
            MaxHealth = health;
            CurrentHealth = health;
            _meshes = meshes;
            _spriteRenderer = spriteRenderer; 
        }

        public void Heal(int hpPoint)
        {
            CurrentHealth += hpPoint;

            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth; 
        }

        public void Damage(int damage)
        {
            CurrentHealth -= damage;
            
            RedDamage();
            
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }

        public void RestoreHealth()
        {
            CurrentHealth = MaxHealth; 
        }

        private void RedDamage()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            if (_meshes != null)
            {
                foreach (var mesh in _meshes)
                {
                    mesh.color = new Color(1f, .34f, .34f);
                    _sequence.InsertCallback(0.5f, () => mesh.color = Color.white);
                }
            }

            if (_spriteRenderer != null)
            {
                _spriteRenderer.color = new Color(1f, .34f, .34f);
                _sequence.InsertCallback(0.5f, () => _spriteRenderer.color = Color.white);
            }
        }
    }
}
