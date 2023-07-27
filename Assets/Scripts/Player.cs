using System.Collections;
using Unity.Mathematics;
using UnityEngine;


public class Player : MonoBehaviour
{
    private float _fireRate = 0.3f;
    private float _canFire = -1;
    private float _xBound = 12.31f;
    private float _yBound = 7.37f;
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _direction;
    
    [SerializeField] private float speed;
    private readonly float _speedMultiplier = 2;
    [SerializeField] private int lives = 3;
    [SerializeField] private int score;

    private bool _isShieldActive;
    private bool _isTripleShotActive;

    [SerializeField] private GameObject playerShield;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleLaserPrefab;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) Debug.LogError("Spawn Manager is Null");
    }

    private void Update()
    {
        PlayerMovement();
        CheckCanFireLaser();
        CheckBound();
    }

    private void PlayerMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _direction = new Vector3(_horizontalInput, _verticalInput, 0);

        transform.Translate(_direction * (speed * Time.deltaTime));
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

    public void SpeedBoostActive()
    {
        speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        speed /=_speedMultiplier;
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            playerShield.SetActive(false);
            return;
        }
        lives--;
        _uiManager.UpdateLives(lives);
        if (lives < 1)
        {
            _uiManager.GameOver();
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        playerShield.SetActive(true);
    }

    public void AddScore(int points)
    {
        score += points;
        _uiManager.UpdateScore(score);
    }
    private void CheckBound()
    {
        // Check X Axis
        if (transform.position.x > _xBound)
        {
            var transform1 = transform;
            transform1.position = new Vector3(-_xBound, transform1.position.y, 0);
        }
        else if (transform.position.x < -_xBound)
        {
            var transform1 = transform;
            transform1.position = new Vector3(_xBound, transform1.position.y, 0);
        }

        // Check Y Axis
        if (transform.position.y > _yBound)
        {
            var transform1 = transform;
            transform1.position = new Vector3(transform1.position.x, -_yBound, 0);
        }
        else if (transform.position.y < -_yBound)
        {
            var transform1 = transform;
            transform1.position = new Vector3(transform1.position.x, _yBound, 0);
        }
    }
}