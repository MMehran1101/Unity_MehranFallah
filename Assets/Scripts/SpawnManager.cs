using System.Collections;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    private bool isStopSpawn = false;
    private float _enemyXPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    
    private IEnumerator SpawnRoutine()
    {
        while (!isStopSpawn)
        {
            EnemyInstantiate();
            yield return new WaitForSeconds(Random.Range(3,5));
        }
    }

    private void EnemyInstantiate()
    {
        _enemyXPos = Random.Range(-9, 9);
        var newEnemy = Instantiate(_enemyPrefab, new Vector3(_enemyXPos, 10, 0)
            , quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
    }

    public void OnPlayerDeath()
    {
        isStopSpawn = true;
    }
}