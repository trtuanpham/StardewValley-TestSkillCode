namespace BugBoxGames.EnhancedEffect
{
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;

    [DisallowMultipleComponent]
    public class EnhancedEffectFadeShow : BaseEffect
    {
        [SerializeField] private Graphic _image;
        [SerializeField] private float _toAlpha = 0;

        protected override Tween OnShowEffect()
        {
            var color = _image.color;
            color.a = 0;
            _image.color = color;
            return _image.DOFade(_toAlpha, _showTime);
        }

        protected override Tween OnHideEffect()
        {
            return _image.DOFade(0, _hideTime);
        }

        private void OnValidate()
        {
            _image = gameObject.GetComponent<Graphic>();
            var color = _image.color;
            _toAlpha = color.a;
        }
    }
}