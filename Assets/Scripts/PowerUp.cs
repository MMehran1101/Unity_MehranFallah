using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    private void LateUpdate()
    {
        if (transform.position.y < -10) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player!=null) player.TripleShotActive();
            Destroy(gameObject);
        }
    }
}