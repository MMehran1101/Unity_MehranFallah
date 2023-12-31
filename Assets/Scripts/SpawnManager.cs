using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private GameObject powerUpContainer;
    [SerializeField] private GameObject enemyContainer;
    private bool _isStopSpawn;
    private float _spawnXPos;
    private int _randomPowerup;
    
    public void StartSpawning()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }
    private IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(2);
        while (!_isStopSpawn)
        {
            EnemyInstantiate();
            yield return new WaitForSeconds(Random.Range(2,5));
        }
    }
    private void EnemyInstantiate()
    {
        _spawnXPos = Random.Range(-9, 9);
        var newEnemy = Instantiate(enemyPrefab, new Vector3(_spawnXPos, 10, 0)
            , quaternion.identity);
        newEnemy.transform.parent = enemyContainer.transform;
    }

    private IEnumerator PowerUpSpawnRoutine()
    {
        yield return new WaitForSeconds(3);
        while (!_isStopSpawn)
        {
            PowerUpInstantiate();
            yield return new WaitForSeconds(Random.Range(5,9));
        }

    }
    private void PowerUpInstantiate()
    {
        _spawnXPos = Random.Range(-9, 9);
        _randomPowerup = Random.Range(0, 3);
        var newPowerUp = Instantiate(powerups[_randomPowerup]
            , new Vector3(_spawnXPos, 10, 0)
            , quaternion.identity);
        newPowerUp.transform.parent = powerUpContainer.transform;
    }

    public void OnPlayerDeath()
    {
        _isStopSpawn = true;
    }
}