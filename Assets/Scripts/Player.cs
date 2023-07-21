using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _lives = 3;
    private float _fireRate = 0.3f;
    private float _canFire = -1;
    private float _enemyXPos, _enemyYPos;
    private float _xBound = 12.31f;
    private float _yBound = 7.37f;

    private void Start()
    {
        InvokeRepeating(nameof(EnemyInstantiate), 1, Random.Range(2, 6));
    }

    private void Update()
    {
        PlayerMovement();
        CheckCanFireLaser();
    }

    public void Damage()
    {
        _lives--;
        if (_lives < 1) Destroy(gameObject);
    }

    private void EnemyInstantiate()
    {
        _enemyXPos = Random.Range(-9, 9);
        _enemyYPos = Random.Range(8, 10);
        Instantiate(_enemyPrefab, new Vector3(_enemyXPos, _enemyYPos, 0)
            , quaternion.identity);
    }

    private void CheckCanFireLaser()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        var offsetPosition = transform.position + new Vector3(0, 1.6f, 0);
        Instantiate(_laserPrefab, offsetPosition, quaternion.identity);
    }

    private void PlayerMovement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        CheckBound();
    }

    private void CheckBound()
    {
        // Check X Axis
        if (transform.position.x > _xBound)
        {
            transform.position = new Vector3(-_xBound, transform.position.y, 0);
        }
        else if (transform.position.x < -_xBound)
        {
            transform.position = new Vector3(_xBound, transform.position.y, 0);
        }

        // Check Y Axis
        if (transform.position.y > _yBound)
        {
            transform.position = new Vector3(transform.position.x, -_yBound, 0);
        }
        else if (transform.position.y < -_yBound)
        {
            transform.position = new Vector3(transform.position.x, _yBound, 0);
        }
    }
}