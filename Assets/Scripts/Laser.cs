using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 10;

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }

    private void LateUpdate()
    {
        if (gameObject.transform.position.y > 10)
        {
            Destroy(transform.parent != null ? transform.parent.gameObject : gameObject);
        }
    }
}