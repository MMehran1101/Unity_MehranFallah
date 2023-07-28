using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4;
    private Player _player;
    private Animator _animator;
    private static readonly int OnEnemyDeath = Animator.StringToHash("OnEnemyDeath");

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null) player.Damage();
            _animator.SetTrigger(OnEnemyDeath);
            _speed = 0;
            Destroy(gameObject, 2.5f);
        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            _animator.SetTrigger(OnEnemyDeath);
            _speed = 0;
            Destroy(gameObject, 2.5f);
        }
    }
}