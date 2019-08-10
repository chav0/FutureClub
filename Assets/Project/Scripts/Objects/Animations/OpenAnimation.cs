using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Objects.Animations
{
    public class OpenWidgetAnimation : MonoBehaviour
    { 
        private Sequence _sequence; 
 
        private void OnEnable()
        {
            _sequence = DOTween.Sequence();
 
            transform.localScale = Vector3.zero;
 
            _sequence.Append(transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack))
                .SetUpdate(UpdateType.Normal, true)
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