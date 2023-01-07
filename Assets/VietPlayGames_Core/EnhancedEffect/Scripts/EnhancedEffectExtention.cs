using BugBoxGames.EnhancedEffect;
using UnityEngine;

public static class EnhancedEffectExtention
{
    public static void EnableWithEffect(this GameObject gameObject)
    {
        BaseEffect[] effects = gameObject.GetComponents<BaseEffect>();
        if (effects.Length > 0)
        {
            gameObject.SetActive(true);
            foreach (var effect in effects)
            {
                effect.ForceShowEffect();
            }
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public static void EnableWithEffect(this MonoBehaviour monoBehaviour)
    {
        monoBehaviour.gameObject.EnableWithEffect();
    }

    public static void DisalbeWithEffect(this GameObject gameObject)
    {
        BaseEffect[] effects = gameObject.GetComponents<BaseEffect>();
        if (effects.Length > 0)
        {
            foreach (var effect in effects)
            {
                effect.HideEffect();
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public static void DisalbeWithEffect(this MonoBehaviour monoBehaviour)
    {
        monoBehaviour.gameObject.DisalbeWithEffect();
    }
}
