using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyContainer;
    private bool _isStopSpawn;
    private float _enemyXPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    
    private IEnumerator SpawnRoutine()
    {
        while (!_isStopSpawn)
        {
            EnemyInstantiate();
            yield return new WaitForSeconds(Random.Range(3,5));
        }
    }

    private void EnemyInstantiate()
    {
        _enemyXPos = Random.Range(-9, 9);
        var newEnemy = Instantiate(enemyPrefab, new Vector3(_enemyXPos, 10, 0)
            , quaternion.identity);
        newEnemy.transform.parent = enemyContainer.transform;
    }

    public void OnPlayerDeath()
    {
        _isStopSpawn = true;
    }
}