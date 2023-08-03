using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int powerUpID;
    [SerializeField] private AudioClip audioClip;

    private void Update()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * speed));
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
            if (player != null)
            {
                AudioSource.PlayClipAtPoint(audioClip,transform.position);
                switch (powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        Debug.LogWarning("Invalid Value set on powerUpID");
                        break;
                }
            }

            Destroy(gameObject);
        }
    }
}