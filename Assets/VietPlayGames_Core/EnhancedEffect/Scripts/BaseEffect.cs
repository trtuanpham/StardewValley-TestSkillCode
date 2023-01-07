namespace BugBoxGames.EnhancedEffect
{
    using DG.Tweening;
    using UnityEngine;

    public class BaseEffect : MonoBehaviour
    {
        [SerializeField] protected Ease _showEase = Ease.OutBack;
        [SerializeField] protected float _showTime = 0.3f;
        [SerializeField] protected Ease _hideEase = Ease.InQuad;
        [SerializeField] protected float _hideTime = 0.3f;
        [SerializeField] public float Delay = 0;
        [SerializeField] private bool _randomDelay = false;
        [SerializeField] bool _autoPlay = true;
        [SerializeField] bool _noUseEffectFirstTime = false;

        private float _time;
        private Tween _tween;

        public float TimeEffect
        {
            get
            {
                return _showTime;
            }
        }

        /// <summary>
        /// Gets the total of delay + time.
        /// </summary>
        public float FullTimeEffect
        {
            get
            {
                return Delay + _showTime;
            }
        }

        private bool _isInitalizeEffect = false;
        private bool _isShowing;
        private bool _isStart;

        protected virtual void Start()
        {
            _isStart = true;
            OnActiveEffect();
        }

        protected virtual void OnEnable()
        {
            if (_isStart)
            {
                OnActiveEffect();
            }
        }

        protected virtual void OnActiveEffect()
        {
            if (_autoPlay)
            {
                ShowEffect();
            }

            _time = Time.time;
        }

        public void TurnOffAuto()
        {
            _autoPlay = false;
        }

        public void ShowEffect()
        {
            if (_isShowing)
            {
                return;
            }
            if (_noUseEffectFirstTime && (Time.time - _time) <= 1)
            {
                return;
            }

            _isShowing = true;

            if (!_isInitalizeEffect)
            {
                _isInitalizeEffect = true;
                OnInitEffect();
            }
            _tween = OnShowEffect().SetDelay(_randomDelay ? UnityEngine.Random.Range(0.1f, 0.5f) + Delay : Delay).SetEase(_showEase);
        }

        public void ForceShowEffect()
        {
            if (!_isInitalizeEffect)
            {
                _isInitalizeEffect = true;
                OnInitEffect();
            }
            _tween = OnShowEffect().SetDelay(_randomDelay ? UnityEngine.Random.Range(0.1f, 0.5f) + Delay : Delay).SetEase(_showEase);
        }

        public void HideEffect(bool hideObject = true)
        {
            _isShowing = false;
            _tween = OnHideEffect().SetEase(_showEase).OnComplete(() =>
            {
                _isShowing = false;
                if (hideObject)
                {
                    gameObject.SetActive(false);
                }
            });
        }

        public void KillTween()
        {
            if (_tween != null)
            {
                _tween.Kill();
            }
        }

        protected virtual void OnDisable()
        {
            _isShowing = false;
            KillTween();
        }

        protected virtual void OnInitEffect() { }

        protected virtual Tween OnShowEffect()
        {
            return null;
        }

        protected virtual Tween OnHideEffect()
        {
            return null;
        }
    }
}