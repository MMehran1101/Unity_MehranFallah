using System.Collections;
using Unity.Mathematics;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleLaserPrefab;
    [SerializeField] private int lives = 3;
    
    private SpawnManager _spawnManager;
    [SerializeField] bool _isTripleShotActive;
    private float _fireRate = 0.3f;
    private float _canFire = -1;
    private float _xBound = 12.31f;
    private float _yBound = 7.37f;
                                            
    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) Debug.LogError("Spawn Manager is Null");
    }

    private void Update()
    {          
        PlayerMovement();
        CheckCanFireLaser();
    }

    public void Damage()
    {
        lives--;
        if (lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
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
        if (_isTripleShotActive)
        {
            Instantiate(tripleLaserPrefab, transform.position, quaternion.identity);
            return;
        }
        var offsetPosition = transform.position + new Vector3(0, 1.1f, 0);
        Instantiate(laserPrefab, offsetPosition, quaternion.identity);
    }

    private void PlayerMovement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * (speed * Time.deltaTime));

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