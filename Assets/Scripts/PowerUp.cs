using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int powerUpID;

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
                switch (powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        Debug.Log("Shield");
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