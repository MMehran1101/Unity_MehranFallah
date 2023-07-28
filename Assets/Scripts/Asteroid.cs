using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    private float _speedOfRotation = 20;
    private float _speed = 3;

    [SerializeField] private GameObject explosion;

    void Update()
    {
        transform.Rotate(0, 0, _speedOfRotation * Time.deltaTime);
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Instantiate(explosion, transform.position,
                quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject,0.5f);
        }
    }
}