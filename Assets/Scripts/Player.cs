using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private GameObject _laserPrefab;

    private float _xBound = 12.31f;
    private float _yBound = 7.37f;

    void Update()
    {
        // instantiate laser
        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(_laserPrefab,transform.position,quaternion.identity);
        }
        PlayerMovement();  
        CheckBound();
    }

    private void PlayerMovement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
    }

    private void CheckBound()
    {
        // Check X Axis
        if (transform.position.x > _xBound)
        {
            transform.position = new Vector3(-_xBound, transform.position.y,0);
        }
        else if (transform.position.x < -_xBound)
        {
            transform.position = new Vector3(_xBound, transform.position.y,0);
        }

        // Check Y Axis
        if (transform.position.y > _yBound)
        {
            transform.position = new Vector3(transform.position.x, -_yBound,0);
        }
        else if (transform.position.y < -_yBound)
        {
            transform.position = new Vector3(transform.position.x, _yBound, 0);
        }
    }
}