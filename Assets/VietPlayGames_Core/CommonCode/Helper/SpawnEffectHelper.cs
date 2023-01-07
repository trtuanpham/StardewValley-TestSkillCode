using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnEffectHelper
{
    public static GameObject Spawn(string id, Transform parent) 
    {
        GameObject gameObject = UnityUtils.SpawnResources("Prefabs/Effects/" + id);
        gameObject.transform.SetParent(parent);
        gameObject.transform.ResetTransform();
        GameObject.Destroy(gameObject, 3);
        return gameObject;
    }
}
