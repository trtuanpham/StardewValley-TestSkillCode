using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayAnimation : MonoBehaviour
{
    [SerializeField] Animation _animation;
    // Start is called before the first frame update
    void Start()
    {
        _animation["sea_ani"].time = Random.Range(0f, 20f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
