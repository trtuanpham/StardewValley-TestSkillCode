namespace BugBoxGames.EnhancedEffect
{
    using UnityEngine;
    using DG.Tweening;

    public class EnhancedEffectCanvasGroupFadeShow : BaseEffect
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] float _fromAlpha = 0;
        [SerializeField] float _toAlpha;

        protected override Tween OnShowEffect()
        {
            _canvasGroup.alpha = _fromAlpha;
            return _canvasGroup.DOFade(_toAlpha, _showTime);
        }

        protected override Tween OnHideEffect()
        {
            return _canvasGroup.DOFade(0, _hideTime);
        }

        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
            _toAlpha = _canvasGroup.alpha;
        }
    }
}