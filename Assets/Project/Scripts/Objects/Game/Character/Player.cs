using System;
using Project.Scripts.Views.UserInput;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

namespace Project.Scripts.Objects.Game.Character
{
    public class Player : Character
    {
        [SerializeField] private int _energy;
        [SerializeField] private Image _energySlider;
        [SerializeField] private float _spendEnergyPerTick;
        public float _spendEnergyForBuilding;
        [SerializeField] private int _hpFromFood;
        [SerializeField] private int _energyFromFood;
        [SerializeField] private Button _eatButton;
        [SerializeField] private GameObject _noEnergyPopup; 

        public UIMessage EatFood = new UIMessage();
        public Energy Energy { get; set; }
        
        private new void Awake()
        {
            base.Awake(); 
            _eatButton.onClick.AddListener(EatFood.Set);
            Energy = new Energy(_energy, _energySlider);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            var coin = col.gameObject.GetComponent<Coin>();
            
            if(coin != null)
                coin.IsCollisionEnter = true;

            var tower = col.gameObject.GetComponent<Place>();
            
            if(tower != null)
                tower.IsCollisionEnter = true;
            
            var seeds = col.gameObject.GetComponent<Seeds>();
            
            if(seeds != null)
                seeds.IsCollisionEnter = true;

            var house = col.gameObject.GetComponent<House>();
            
            if(house != null)
                house.IsCollisionEnter = true;
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            var tower = col.gameObject.GetComponent<Place>();
            
            if(tower != null)
                tower.IsCollisionEnter = false;

            var house = col.gameObject.GetComponent<House>();
            
            if(house != null)
                house.IsCollisionEnter = false;
        }

        public void Move(Direction direction, float multiplier = 1f)
        {
            if (direction != Direction.None)
            {
                if (Energy.CurrentEnergy > 0f)
                {
                    Energy.SpendEnergy(_spendEnergyPerTick * (multiplier - 1f) * Time.deltaTime);
                }
                else
                {
                    if (multiplier > 1f)
                    {
                        multiplier = 1f;
                        ShowLowEnergy();
                    } 
                }
            }
            
            Transform.Move(direction, multiplier);
        }

        public void Build()
        {
            Energy.SpendEnergy(_spendEnergyForBuilding); 
        }

        public void Eat()
        {
            if (Math.Abs(Energy.CurrentEnergy - Energy.MaxEnergy) < 0.8f)
            {
                Health.Heal(_hpFromFood);
            }
            else
            {
                Energy.AddEnergy(_energyFromFood); 
            }
        }

        public void ShowLowEnergy()
        {
            _noEnergyPopup.SetActive(true);
        }
    }
}
