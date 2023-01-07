using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnEffectHelper
{
    public static GameObject Spawn(string id) 
    {
        GameObject gameObject = UnityUtils.SpawnResources("Effects/" + id);
        gameObject.transform.ResetTransform();
        GameObject.Destroy(gameObject, 3);
        return gameObject;
    }
}
