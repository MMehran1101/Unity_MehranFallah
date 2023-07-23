using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    private float _enemyXPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            EnemyInstantiate();
            yield return new WaitForSeconds(Random.Range(3,5));
        }
    }

    private void EnemyInstantiate()
    {
        _enemyXPos = Random.Range(-9, 9);
        Instantiate(_enemyPrefab, new Vector3(_enemyXPos, 10, 0)
            , quaternion.identity);
    }
}