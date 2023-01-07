namespace BugBoxGames.EnhancedEffect
{
    using UnityEngine;
    using DG.Tweening;

    [DisallowMultipleComponent]
    public class EnhancedEffectScaleShow : BaseEffect
    {
        [SerializeField] private Vector3 _fromValue = new Vector3(0, 0, 1);
        [SerializeField] private Vector3 _toValue;

        protected override Tween OnShowEffect()
        {
            transform.localScale = _fromValue;
            return transform.DOScale(_toValue, _showTime);
        }

        protected override Tween OnHideEffect()
        {
            return transform.DOScale(0, _hideTime);
        }

        private void OnValidate()
        {
            _toValue = transform.localScale;
        }
    }
}