using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        // This part available on course, but I don't use it because it's useless
        /*if (transform.position.y<-9)
        {
            transform.position = new Vector3(Random.Range(-9, 9),
                Random.Range(8, 10), 0);
        } */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if(player!=null) player.Damage();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}