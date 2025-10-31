using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int[] _count;
    [SerializeField] private float _enemySpawnInterval;
   

    public GameObject EnemyPrefab => _enemyPrefab;
    public int[] Count => _count;
    public float EnemySpawnInterval => _enemySpawnInterval;
  

}
