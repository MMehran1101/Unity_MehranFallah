using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    private float _fireRate = 0.3f;
    private float _canFire = -1;
    private float _xBound = 12.31f;
    private float _yBound = 4.5f;
    private float _clampYAxis;
    private float _horizontalInput,_verticalInput;
    
    private Vector3 _direction;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    
    private readonly float _speedMultiplier = 2;
    [Header("Player Properties")]
    [SerializeField] private float speed;
    [SerializeField] private int lives = 3;
    [SerializeField] private int score;

    [Header("State of Powerups")]
    [SerializeField]private bool isShieldActive;
    [SerializeField]private bool isTripleShotActive;

    [Header("Game Objects")]
    [SerializeField] private GameObject playerShield;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleLaserPrefab;
    [SerializeField] private GameObject rightEngine,leftEngine;
    
    private AudioSource _audioSource;
    [Header("Audios")]
    [SerializeField] private AudioClip laserAudioClip;
    [SerializeField] private AudioClip explosionAudioClip;


    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        CheckGameObjectNull(_uiManager, "UI Manager");
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        CheckGameObjectNull(_spawnManager, "Spawn Manager");
        _audioSource = GetComponent<AudioSource>();
        CheckGameObjectNull(_audioSource, "Audio Source");        

    }

    private void CheckGameObjectNull(object myObject,string nameOfObject)
    {
        if (myObject == null) 
            ShooterLogger.LogError($"{nameOfObject} is NULL.");
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
        _audioSource.clip = laserAudioClip;
        _canFire = Time.time + _fireRate;
        CheckIsTripleShot();
        _audioSource.Play();
    }

    private void CheckIsTripleShot()
    {
        if (isTripleShotActive)
        {
            Instantiate(tripleLaserPrefab, transform.position, quaternion.identity);
        }
        else
        {
            var offsetPosition = transform.position + new Vector3(0, 1.1f, 0);
            Instantiate(laserPrefab, offsetPosition, quaternion.identity);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        speed /= _speedMultiplier;
    }

    public void Damage()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            playerShield.SetActive(false);
            return;
        }

        lives--;

        if (lives == 2) rightEngine.SetActive(true);
        else if (lives == 1) leftEngine.SetActive(true);

        _uiManager.UpdateLives(lives);
        if (lives < 1)
        {
            _audioSource.clip = explosionAudioClip;
            _audioSource.Play();
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void ShieldActive()
    {
        isShieldActive = true;
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
        _clampYAxis = Mathf.Clamp(transform.position.y, -_yBound, _yBound);
        transform.position = new Vector3(transform.position.x, _clampYAxis, 0);
    }
}