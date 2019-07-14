﻿using UnityEngine;

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

        public void Damage(int damage)
        {
            CurrentHealth -= damage; 
        }
    }
}