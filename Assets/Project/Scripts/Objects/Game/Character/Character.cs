using UnityEngine;

namespace Project.Scripts.Objects.Game.Character
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _health;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            Health = new Health(_health);
            Transform = new CTransform(transform, _speed, _animator);
        }
        
        public Health Health;
        public CTransform Transform; 
    }
}
