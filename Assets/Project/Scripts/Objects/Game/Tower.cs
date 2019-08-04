using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Objects.Game.Character;
using Project.Scripts.Views.UserInput;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Scripts.Objects.Game
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private int _health; 
        [SerializeField] private int _cost; 
        [SerializeField] private Slider _healthBar;

        public int Damage;

        public Health Health;
        public int Cost => _cost;

        private void Update()
        {
            _healthBar.gameObject.SetActive(Health.CurrentHealth  != Health.MaxHealth);
            _healthBar.value = Health.CurrentHealth / (float) Health.MaxHealth; 
        }

        private void OnEnable()
        {
            Health = new Health(_health);
        }

        private void OnDisable()
        {
            _healthBar.gameObject.SetActive(false);
        }
    }
}
