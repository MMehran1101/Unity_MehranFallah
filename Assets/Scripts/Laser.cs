using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8;

    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }

    void LateUpdate()
    {
        if (gameObject.transform.position.y> 10)
        {
            Destroy(gameObject);
        }
    }
}