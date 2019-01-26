using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _trashCanPrefab, _housePrefab;
    [SerializeField]
    private GameObject[] _treePrefabs, _bottlePrefabs;

    [SerializeField]
    private int _housePoolSize, _treesPoolSize, _trashCansPoolSize, _bottlesPoolSize;

    private Queue<GameObject> _trashCansPool, _housesPool, _treesPool, _bottlesPool;



    private List<GameObject> _activeMovingObjects;
    public List<GameObject> ActiveMovingObjects { get => _activeMovingObjects;}

    [SerializeField]
    private int _spawnPositionX,
        _houseClosestSpawnDist, _treeClosestSpawnDist, _trashcanClosestSpawnDist,
        _houseSpawnerZPosition, _trashcanSpawnerZPosition, _treeSpawnerPosition,
        _treeSpawnProbability, _houseSpawnProbability, _trashcanSpawnProbability;

    private Transform _lastHouse, _lastTrashcan;

    [SerializeField]
    private int _initialTrashcanSpawnTimer;

    private const int SpawnTimerDelay = 1;
    private WaitForSeconds _spawnTimerWait;



    // Start is called before the first frame update
    void Start()
    {
        _trashCansPool = new Queue<GameObject>();
        _housesPool = new Queue<GameObject>();
        _treesPool = new Queue<GameObject>();
        _bottlesPool = new Queue<GameObject>();

        _activeMovingObjects = new List<GameObject>();

        PopulatePool(_trashCansPool, _trashCanPrefab, _trashCansPoolSize);
        PopulatePool(_housesPool, _housePrefab, _housePoolSize);
        PopulatePool(_treesPool, _treePrefabs, _treesPoolSize);
        PopulatePool(_bottlesPool, _bottlePrefabs, _bottlesPoolSize);

        _lastHouse = GetObjectFromPool(_housesPool).transform;
        _lastTrashcan = GetObjectFromPool(_trashCansPool).transform;

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
        obj.SetActive(true);
        pool.Enqueue(obj);
        return obj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        _activeMovingObjects.Remove(obj);
        obj.SetActive(false);
    }

    public GameObject GetBottle()
    {
        return GetObjectFromPool(_bottlesPool);
    }

    public void DestroyBottle(GameObject bottle)
    {
        ReturnObjectToPool(bottle);
    }

    private IEnumerator SpawnCoroutine()
    {

        while (true)
        {
            if (_lastHouse.position.x > _spawnPositionX + _houseClosestSpawnDist && Random.Range(0, 100) > _houseSpawnProbability)
                _lastHouse = SpawnObjectFromPool(_housesPool, _houseSpawnerZPosition);

            if (_initialTrashcanSpawnTimer > 0)
                _initialTrashcanSpawnTimer--;
            else if (_lastTrashcan.position.x > _spawnPositionX + _trashcanClosestSpawnDist && Random.Range(0,100) > _trashcanSpawnProbability)
                _lastTrashcan = SpawnObjectFromPool(_trashCansPool, _trashcanSpawnerZPosition);

            yield return _spawnTimerWait;
        }
    }

    private Transform SpawnObjectFromPool(Queue<GameObject> pool, int ZPosition)
    {
        Transform obj = GetObjectFromPool(pool).transform;
        obj.position = new Vector3(_spawnPositionX, 0, -ZPosition + (2 * Random.Range(0, 2)) * ZPosition);
        return obj;
    }

    private int ChangeSpawnDelay(int baseSpawnDelay, int maxSpawnDelay)
    {
        return Random.Range(baseSpawnDelay, maxSpawnDelay + 1);
    }
}
