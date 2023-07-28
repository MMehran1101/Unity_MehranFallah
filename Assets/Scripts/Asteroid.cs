using Unity.Mathematics;
using UnityEngine;


public class Asteroid : MonoBehaviour
{
    private float _speedOfRotation = 20;
    private SpawnManager _spawnManager;
    [SerializeField] private GameObject explosion;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        transform.Rotate(0, 0, _speedOfRotation * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Instantiate(explosion, transform.position, quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.5f);
        }
    }
}