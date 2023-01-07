namespace BugBoxGames.EnhancedEffect
{
    using UnityEngine;
    using DG.Tweening;

    [DisallowMultipleComponent]
    public class EnhancedEffectMoveShow : BaseEffect
    {
        [SerializeField] private DirectionMove _moveType = DirectionMove.Left;
        [SerializeField] private float offset = 0;
        [SerializeField] private Vector2 _hidePosition;
        [SerializeField] private Vector2 _showPosition;

        private RectTransform rectTransform
        {
            get
            {
                return transform as RectTransform;
            }
        }

        protected override Tween OnShowEffect()
        {
            rectTransform.anchoredPosition = _hidePosition;
            return rectTransform.DOAnchorPos(_showPosition, _showTime);
        }

        protected override Tween OnHideEffect()
        {
            return rectTransform.DOAnchorPos(_hidePosition, _hideTime);
        }

        private void OnValidate()
        {
            //_showPosition = rectTransform.anchoredPosition;
            _hidePosition = rectTransform.anchoredPosition;

            //if (rectTransform.anchorMax.x == 0.5f && rectTransform.anchorMax.y == 1f && rectTransform.anchorMin.x == 0.5f && rectTransform.anchorMin.y == 1f)
            //{
            //    _moveType = DirectionMove.Down;
            //}
            //else if (rectTransform.anchorMax.x == 0f && rectTransform.anchorMax.y == 0.5f && rectTransform.anchorMin.x == 0f && rectTransform.anchorMin.y == 0.5f)
            //{
            //    _moveType = DirectionMove.Left;
            //}
            //else if (rectTransform.anchorMax.x == 0.5f && rectTransform.anchorMax.y == 0f && rectTransform.anchorMin.x == 0.5f && rectTransform.anchorMin.y == 0f)
            //{
            //    _moveType = DirectionMove.Up;
            //}
            //else if (rectTransform.anchorMax.x == 1f && rectTransform.anchorMax.y == 0.5f && rectTransform.anchorMin.x == 1f && rectTransform.anchorMin.y == 0.5f)
            //{
            //    _moveType = DirectionMove.Right;
            //}

            if (offset == 0)
            {
                switch (_moveType)
                {
                    case DirectionMove.Left:
                    case DirectionMove.Right:
                        offset = rectTransform.rect.width;
                        break;
                    case DirectionMove.Up:
                    case DirectionMove.Down:
                        offset = rectTransform.rect.height;
                        break;
                }
            }

            switch (_moveType)
            {
                case DirectionMove.Left:
                    _hidePosition.x -= offset;
                    break;
                case DirectionMove.Right:
                    _hidePosition.x += offset;
                    break;
                case DirectionMove.Up:
                    _hidePosition.y -= offset;
                    break;
                case DirectionMove.Down:
                    _hidePosition.y += offset;
                    break;
            }

            _showPosition = rectTransform.anchoredPosition;
        }

        public enum DirectionMove
        {
            Left, Right, Up, Down
        }
    }
}