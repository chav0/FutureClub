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
        [SerializeField] private float _delay;
        [SerializeField] private Vector3 _pointPotatoSpawn;
        [SerializeField] private SpriteRenderer _sprite;

        public float Range; 

        public Potato PotatoPrefab;

        public Health Health;
        public int Cost => _cost;

        private float _timer; 
        private void Update()
        {
            _healthBar.gameObject.SetActive(Health.CurrentHealth  != Health.MaxHealth);
            _healthBar.value = Health.CurrentHealth / (float) Health.MaxHealth; 
        }

        private void OnEnable()
        {
            Health = new Health(_health, null, _sprite);
        }

        private void OnDisable()
        {
            _healthBar.gameObject.SetActive(false);
        }

        public void Attack(Vector3 enemyPosition)
        {
            if (Time.time - _timer >= _delay)
            {
                var potato = Instantiate(PotatoPrefab, transform);
                potato.gameObject.SetActive(true);
                potato.transform.localPosition = _pointPotatoSpawn; 
                potato.Direction = (enemyPosition - potato.transform.position).normalized;
                _timer = Time.time; 
            }
        }
    }
}
