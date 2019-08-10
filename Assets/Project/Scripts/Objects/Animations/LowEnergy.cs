using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Objects.Animations
{
    public class LowEnergy : MonoBehaviour
    {
        private Sequence _sequence; 
 
        private void OnEnable()
        {
            _sequence = DOTween.Sequence();
 
            transform.localScale = Vector3.zero;
 
            _sequence.Append(transform.DOScale(1f, 0.25f).SetEase(Ease.OutBack))
                .AppendInterval(2f)
                .Append(transform.DOScale(0f, 0.25f).SetEase(Ease.OutBack))
                .AppendCallback(() => gameObject.SetActive(false))
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
