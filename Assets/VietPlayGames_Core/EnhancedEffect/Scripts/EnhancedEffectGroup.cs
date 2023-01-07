namespace BugBoxGames.EnhancedEffect
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    public class EnhancedEffectGroup : MonoBehaviour
    {
        [SerializeField] List<BaseEffect> _effects;
        [SerializeField] float _delay = 0f;
        [SerializeField] float _delayStep = 0.2f;

        public float Delay
        {
            get
            {
                return _delay;
            }
        }

        public float DelayStep
        {
            get
            {
                return _delayStep;
            }
        }

        private void Awake()
        {
            foreach (var effect in _effects)
            {
                effect.ShowEffect();
            }
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(EnhancedEffectGroup))]
        public class EnhancedEffectGroupEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (GUILayout.Button("Update"))
                {
                    var enhancedEffectGroup = target as EnhancedEffectGroup;
                    if (enhancedEffectGroup._effects == null)
                    {
                        enhancedEffectGroup._effects = new List<BaseEffect>();
                    }
                    enhancedEffectGroup._effects.Clear();
                    FindEffectOnTransform(enhancedEffectGroup, enhancedEffectGroup.transform, enhancedEffectGroup.Delay, enhancedEffectGroup.DelayStep);
                    FindEffect(enhancedEffectGroup, enhancedEffectGroup.transform, enhancedEffectGroup.Delay + enhancedEffectGroup.DelayStep, enhancedEffectGroup.DelayStep);
                }
            }

            private void FindEffect(EnhancedEffectGroup enhancedEffectGroup, Transform transf, float delay, float delayStep)
            {
                foreach (Transform tra in transf)
                {
                    FindEffectOnTransform(enhancedEffectGroup, tra, delay, delayStep);
                    delay += delayStep;
                    FindEffect(enhancedEffectGroup, tra, delay + delayStep, delayStep);
                }
            }

            private void FindEffectOnTransform(EnhancedEffectGroup enhancedEffectGroup, Transform transform, float delay, float delayStep)
            {
                var arrayEffect = transform.GetComponents<BaseEffect>();
                foreach (var effect in arrayEffect)
                {
                    effect.Delay = delay;
                }

                enhancedEffectGroup._effects.AddRange(arrayEffect);
            }
        }
#endif
    }
}