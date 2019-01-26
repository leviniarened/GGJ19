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
    private int _spawnDistance, _houseSpawnBaseDelay, _trashcanSpawnBaseDelay, _treeSpawnBaseDelay,
        _houseSpawnDelayMax, _treeSpawnDelayMax, _trashcanSpawnDelayMax,
        _houseSpawnerPosition, _trashcanSpawnerPosition, _treeSpawnerPosition,
        _treeSpawnProbability, _trashcanSpawnProbability;

    private int _houseSpawnDelay, _trashcanSpawnDelay, _treeSpawnDelay;

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

        _spawnTimerWait = new WaitForSeconds(SpawnTimerDelay);

        _houseSpawnDelay = _houseSpawnBaseDelay;
        _trashcanSpawnDelay = _trashcanSpawnBaseDelay;
        _treeSpawnDelay = _treeSpawnBaseDelay;

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
            if (_houseSpawnDelay-- == 0)
            {
                SpawnObjectFromPool(_housesPool, _houseSpawnerPosition);
                _houseSpawnDelay = ChangeSpawnDelay(_houseSpawnBaseDelay, _houseSpawnDelayMax);
            }

            if (_treeSpawnDelay-- == 0)
            {
                SpawnObjectFromPool(_treesPool, _treeSpawnerPosition);
                _treeSpawnDelay = ChangeSpawnDelay(_treeSpawnBaseDelay, _treeSpawnDelayMax);
            }
            
            if (_trashcanSpawnDelay-- == 0)
            {
                SpawnObjectFromPool(_trashCansPool, _trashcanSpawnerPosition);
                _trashcanSpawnDelay = ChangeSpawnDelay(_trashcanSpawnBaseDelay, _trashcanSpawnDelayMax);
            }


            yield return _spawnTimerWait;
        }
    }

    private void SpawnObjectFromPool(Queue<GameObject> pool, int ZPosition)
    {
        GetObjectFromPool(pool).transform.position = new Vector3(_spawnDistance, 0, -ZPosition + (2 * Random.Range(0, 2)) * ZPosition);
    }

    private int ChangeSpawnDelay(int baseSpawnDelay, int maxSpawnDelay)
    {
        return Random.Range(baseSpawnDelay, maxSpawnDelay + 1);
    }
}
