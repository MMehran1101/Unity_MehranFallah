using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4;
    private Player _player;
    private Animator _animator;
    private static readonly int OnEnemyDeath = Animator.StringToHash("OnEnemyDeath");
    private AudioSource _explosionAudioSource;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) ShooterLogger.LogError("Player is NUll.");
        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null) ShooterLogger.LogError("Player Animator is NULL.");
        _explosionAudioSource = gameObject.GetComponent<AudioSource>();
        if (_explosionAudioSource == null) ShooterLogger.LogError("Explosion Audio is NULL.");
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
            _explosionAudioSource.Play();
            Destroy(gameObject, 2.5f);
        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            _player.AddScore(10);
            _animator.SetTrigger(OnEnemyDeath);
            _speed = 0;
            _explosionAudioSource.Play();
            Destroy(gameObject, 2.5f);
        }
    }
}