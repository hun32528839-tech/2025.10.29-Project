using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{ 
    [SerializeField] private Wave[] _wave;
    [SerializeField] private Transform[] _enemySpawnPoint;

    private void Start()
    {
        StartCoroutine(EnemySpawnPoint());
    }
 
    IEnumerator EnemySpawnPoint()
    {       
        for (int i = 0; i < _wave.Length; i++)
        {
            Wave currentWave = _wave[i];

            for (int j = 0; j < currentWave.Count[i]; j++)
            {
                Transform spawnPoint = _enemySpawnPoint[Random.Range(0, _enemySpawnPoint.Length)];
                Instantiate(currentWave.EnemyPrefab, spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(currentWave.EnemySpawnInterval);
            }                
        }        
    }
}
