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
        [SerializeField] private Button _button; 
        [SerializeField] private GameObject _ruin;
        [SerializeField] private GameObject _building;  
        
        private bool _isCollisionEnter;
        
        public Health Health;
        public TowerState State { get; set; }
        public int Cost => _cost;
        public UIMessage Build = new UIMessage();


        public bool IsCollisionEnter
        {
            get => _isCollisionEnter;
            set
            {
                _button.gameObject.SetActive(value && State == TowerState.Ruin);
                _isCollisionEnter = value;
            }
        }

        private void Awake()
        {
            Health = new Health(_health);
            Health.CurrentHealth = 0; 
            State = TowerState.Ruin; 
            _button.onClick.AddListener(Build.Set);
        }

        public void ChangeState(TowerState newState)
        {
            if (newState == State) return;
            
            switch (State)
            {
                case TowerState.Ruin:
                    State = TowerState.Building;
                    Health.CurrentHealth = _health;
                    _ruin.SetActive(false);
                    _building.SetActive(true);
                    _button.gameObject.SetActive(false);
                    break;
                case TowerState.Building:
                    Health.CurrentHealth = 0; 
                    State = TowerState.Ruin;
                    _ruin.SetActive(true);
                    _building.SetActive(false);
                    break;
            }
        }
    }

    public enum TowerState
    {
        Ruin,
        Building,
    }
}
