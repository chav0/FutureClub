using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Objects.Game.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _health;
        [SerializeField] private Animator _animator;
        [SerializeField] private Slider _healthBar; 
        [SerializeField] private Canvas _canvas; 
        
        private void Awake()
        {
            Health = new Health(_health);
            Transform = new CTransform(transform, _speed, _animator, _canvas);
        }

        private void Update()
        {
            if (_healthBar != null)
            {
                _healthBar.gameObject.SetActive(Health.CurrentHealth  != Health.MaxHealth);
                _healthBar.value = Health.CurrentHealth / (float) Health.MaxHealth; 
            }
        }

        public void SetState(string state)
        {
            _animator.SetTrigger(state);
        }
        
        public Health Health;
        public CTransform Transform; 
    }
}
