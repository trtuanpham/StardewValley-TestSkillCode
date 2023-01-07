using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f;
    //Axis used for rotation;
    [SerializeField] Vector3 rotationAxis = new Vector3(0f, 0f, 1f);

    //Update;
    void Update()
    {
        //Rotate object;
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}

