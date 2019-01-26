using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _trashCanPrefab, _housePrefab;
    [SerializeField]
    private GameObject[] _treePrefabs;

    [SerializeField]
    private int _housePoolSize, _treesPoolSize, _trashCansPoolSize;

    private Queue<GameObject> _trashCansPool, _housesPool, _treesPool;



    private List<GameObject> _activeMovingObjects;
    public List<GameObject> ActiveMovingObjects { get => _activeMovingObjects;}

    [SerializeField]
    private int _spawnDistance, _houseSpawnDelay, _trashcanSpawnDelay, _treeSpawnDelay,
        _houseSpawnerPosition, _trashcanSpawnerPosition, _treeSpawnerPosition,
        _treeSpawnProbability, _trashcanSpawnProbability;

    private const int SpawnTimerDelay = 1;
    private WaitForSeconds _spawnTimerWait;



    // Start is called before the first frame update
    void Start()
    {
        _trashCansPool = new Queue<GameObject>();
        _housesPool = new Queue<GameObject>();
        _treesPool = new Queue<GameObject>();

        _activeMovingObjects = new List<GameObject>();

        PopulatePool(_trashCansPool, _trashCanPrefab, _trashCansPoolSize);
        PopulatePool(_housesPool, _housePrefab, _housePoolSize);
        PopulatePool(_treesPool, _treePrefabs, _treesPoolSize);

        _spawnTimerWait = new WaitForSeconds(SpawnTimerDelay);

        StartCoroutine("SpawnCoroutine");
    }

    private void PopulatePool(Queue<GameObject> pool, GameObject prefab, int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
            pool.Enqueue(instance);
            instance.SetActive(false);
        }
    }

    private void PopulatePool(Queue<GameObject> pool, GameObject[] prefabs, int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject instance = Instantiate(prefabs[Random.Range(0, prefabs.Length)], Vector3.zero, Quaternion.identity, transform);
            pool.Enqueue(instance);
            instance.SetActive(false);
        }
    }

    private GameObject GetObjectFromPool(Queue<GameObject> pool)
    {
        GameObject obj = pool.Dequeue();
        _activeMovingObjects.Add(obj);
        obj.transform.rotation = Quaternion.identity;//восстанавливаем положение объекта - для перевернутого бака например.
        obj.SetActive(true);
        pool.Enqueue(obj);
        return obj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        _activeMovingObjects.Remove(obj);
        obj.SetActive(false);
    }

    private IEnumerator SpawnCoroutine()
    {
        int spawnTimer = 0;
        while (true)
        {
            spawnTimer++;

            if (spawnTimer % _houseSpawnDelay == 0)
                SpawnObjectFromPool(_housesPool, _houseSpawnerPosition);

            if (spawnTimer % _treeSpawnDelay == 0 && Random.Range(0, 100) < _treeSpawnProbability)
                SpawnObjectFromPool(_treesPool, _treeSpawnerPosition);

            if (spawnTimer % _trashcanSpawnDelay == 0 && Random.Range(0, 100) < _trashcanSpawnProbability)
                SpawnObjectFromPool(_trashCansPool, _trashcanSpawnerPosition);

            yield return _spawnTimerWait;
        }
    }

    private void SpawnObjectFromPool(Queue<GameObject> pool, int ZPosition)
    {
        GetObjectFromPool(pool).transform.position = new Vector3(_spawnDistance, 0, -ZPosition + (2 * Random.Range(0, 2)) * ZPosition);
    }
}
