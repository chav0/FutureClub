using DG.Tweening;
using Project.Scripts.Views;
using UnityEngine;

namespace Project.Scripts.Objects.Game
{
    public class Player : MonoBehaviour
    {
        public GameObject Model; 
        
        private bool _isMoving; 
        private readonly float _delta = 2.45f;
        private Sequence _sequence;
        private int level; 
        
        public bool IsGameOver { get; private set; }
        
        private void OnTriggerEnter(Collider other)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                IsGameOver = true; 
            }

            Coin coin = other.GetComponent<Coin>();
            if (coin != null)
            {
                coin.IsCollisionEnter = true; 
            }
        }

        public void Move(Direction direction)
        {
            if (!_isMoving)
            {
                _isMoving = true;
                transform.localPosition = new Vector3(_delta * level, transform.localPosition.y, transform.localPosition.z);
                level += direction == Direction.Left ? 1 : -1; 
                
                if (_sequence != null)
                    _sequence.Kill();

                _sequence = DOTween.Sequence();

                _sequence.Append(transform.DOLocalMoveX(_delta * level, .5f).SetEase(Ease.OutCubic));
                    /*.Join(Model.transform.DOScaleX(0.8f, .2f))
                    .Join(Model.transform.DOScaleZ(0.8f, .2f))
                    .Join(Model.transform.DOScaleY(1.4f, .2f))
                    .Insert(.3f, Model.transform.DOScale(1f, .2f));*/
                _sequence.AppendCallback(() => _isMoving = false); 

                _sequence.Play(); 
            }
        }
    }
}
