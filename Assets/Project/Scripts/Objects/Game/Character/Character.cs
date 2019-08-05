using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Objects.Game.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _health;
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _healthBar; 
        [SerializeField] private Canvas _canvas;

        public void Awake()
        {
            Health = new Health(_health);
            Transform = new CTransform(transform, _speed, _animator, _canvas);
        }

        private void Update()
        {
            if (_healthBar != null)
            {
                _healthBar.fillAmount = Health.CurrentHealth / (float) Health.MaxHealth; 
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
