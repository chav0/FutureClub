using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Objects.Game
{
    public class Potato : MonoBehaviour
    {
        [SerializeField] private Collider2D _potato;
        [SerializeField] private SpriteRenderer _potatoSprite;
        [SerializeField] private ParticleSystem _particles; 
        [SerializeField] private float _speed; 
        public int Damage;

        private Sequence _sequence; 
        public bool IsCollisionEnemy { get; set; }
        public Vector3 Direction { get; set; }
        private void Update()
        {
            if (transform.position.y < -26f || IsCollisionEnemy)
            {
                _potato.enabled = false;
                _potatoSprite.enabled = false; 
                _particles.gameObject.SetActive(true);

                if (_sequence == null)
                {
                    _particles.Play();
                    _sequence = DOTween.Sequence()
                        .InsertCallback(3f, () =>
                        {
                            _sequence.Kill();
                            Destroy(gameObject);
                        });
                }
            }
            else
            {
                Move();
            }
        }

        private void Move()
        {
            transform.position += (Direction * _speed + Vector3.down * 10f) * Time.deltaTime; 
        }
    }
}
