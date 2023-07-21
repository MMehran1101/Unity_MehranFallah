using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8;

    private void Start()
    {
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }

    private void LateUpdate()
    {
        if (gameObject.transform.position.y> 10)
        {
            Destroy(gameObject);
        }
    }
}