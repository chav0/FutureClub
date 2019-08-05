using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Objects.Game.Character
{
    public class Energy
    {
        public float CurrentEnergy;
        public readonly float MaxEnergy;

        private Image _slider; 
        
        public bool IsTired => CurrentEnergy <= 0f;

        public Energy(int energy, Image slider)
        {
            MaxEnergy = energy;
            CurrentEnergy = energy;
            _slider = slider; 
        }

        public void AddEnergy(float value)
        {
            CurrentEnergy += value;

            if (CurrentEnergy > MaxEnergy)
                CurrentEnergy = MaxEnergy;

            _slider.fillAmount = CurrentEnergy / MaxEnergy; 
        }
        
        public void SpendEnergy(float spendValue)
        {
            CurrentEnergy -= spendValue;

            if (CurrentEnergy < 0)
                CurrentEnergy = 0f;

            _slider.fillAmount = CurrentEnergy / MaxEnergy; 
        }

        public void Restore()
        {
            CurrentEnergy = MaxEnergy;
            _slider.fillAmount = 1f; 
        }
    }
}
