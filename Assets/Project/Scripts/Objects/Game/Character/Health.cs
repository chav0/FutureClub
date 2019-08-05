using UnityEngine;

namespace Project.Scripts.Objects.Game.Character
{
    public class Health 
    {
        public int CurrentHealth;
        public int MaxHealth;
        public bool IsDead => CurrentHealth <= 0;

        public Health(int health)
        {
            MaxHealth = health;
            CurrentHealth = health; 
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

            if (CurrentHealth < 0)
                CurrentHealth = 0; 
        }

        public void RestoreHealth()
        {
            CurrentHealth = MaxHealth; 
        }
    }
}
