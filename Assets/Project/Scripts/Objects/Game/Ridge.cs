using System.Collections;
using System.Collections.Generic;
using Project.Scripts.Objects.Game.Character;
using Project.Scripts.Views.UserInput;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Scripts.Objects.Game
{
    public class Ridge : MonoBehaviour
    {
        [SerializeField] private int _health; 
        [SerializeField] private int _cost; 
        [SerializeField] private Slider _healthBar;
        [SerializeField] private float _growTime;
        [SerializeField] private GameObject _firstState;
        [SerializeField] private GameObject _readyState;
        [SerializeField] private SpriteRenderer _sprite;

        private float _timer; 
        
        public Health Health;
        public RidgeState State { get; set; }
        public int Cost => _cost;

        public void Plant()
        {
            State = RidgeState.First; 
            _timer = Time.time; 
        }

        public void SetEmpty()
        {
            State = RidgeState.Empty;
            ChangeState();
        }

        private void Update()
        {
            _healthBar.gameObject.SetActive(Health.CurrentHealth  != Health.MaxHealth);
            _healthBar.value = Health.CurrentHealth / (float) Health.MaxHealth;

            if (State == RidgeState.First)
            {
                State = RidgeState.Second;
                ChangeState(); 
            }

            if (Time.time - _timer > _growTime && State == RidgeState.Second)
            {
                State = RidgeState.Ready;
                ChangeState(); 
            }
        }

        private void ChangeState()
        {
            _firstState.SetActive(State == RidgeState.Second);
            _readyState.SetActive(State == RidgeState.Ready);
        }

        private void OnEnable()
        {
            Health = new Health(_health, null, _sprite);
        }

        private void OnDisable()
        {
            _healthBar.gameObject.SetActive(false);
        }
    }

    public enum RidgeState
    {
        Empty,
        First,
        Second,
        Ready,
    }
}
