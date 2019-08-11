using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Objects.Animations
{
    public class ArrowAnimation : MonoBehaviour
    {
        [SerializeField] private Vector2 _startPos;
        [SerializeField] private Vector2 _endPos;
        [SerializeField] private RectTransform _rect;
        
        private Sequence _sequence; 
 
        private void OnEnable()
        {
            _sequence = DOTween.Sequence();

            _rect.anchoredPosition = _startPos; 
 
            _sequence.Append(_rect.DOAnchorPos(_endPos, 1f))
                .SetLoops(-1, LoopType.Yoyo)
                .Play();
        }
 
        private void OnDisable()
        {
            transform.localScale = Vector3.one;
            _sequence?.Kill(); 
        }
 
        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
