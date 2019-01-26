using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _trashCanPrefab, _streetSegmentPrefab;
    [SerializeField]
    private GameObject[] _treePrefabs, _bottlePrefabs;

    [SerializeField]
    private int _streetSegmentsPoolSize, _trashCansPoolSize, _bottlesPoolSize;

    private Queue<GameObject> _trashCansPool, _streetSegmentsPool, _bottlesPool;

    [SerializeField]
    private int _amountOfStreetSegmentsAtStart, _distBetweenStreetSegmentsAtStart;

    private List<GameObject> _activeMovingObjects;
    public List<GameObject> ActiveMovingObjects { get => _activeMovingObjects;}

    [SerializeField]
    private int _spawnPositionX,
        _streetSegmentClosestSpawnDist, _trashcanClosestSpawnDist,
        _trashcanSpawnerZPosition,
        _streetSegmentSpawnProbability, _trashcanSpawnProbability;

    private Transform _lastStreetSegment, _lastTrashcan;

    [SerializeField]
    private int _initialTrashcanSpawnTimer;

    private const int SpawnTimerDelay = 1;
    



    // Start is called before the first frame update
    void Start()
    {
        _trashCansPool = new Queue<GameObject>();
        _streetSegmentsPool = new Queue<GameObject>();
        _bottlesPool = new Queue<GameObject>();

        _activeMovingObjects = new List<GameObject>();

        PopulatePool(_trashCansPool, _trashCanPrefab, _trashCansPoolSize);
        PopulatePool(_streetSegmentsPool, _streetSegmentPrefab, _streetSegmentsPoolSize);
        PopulatePool(_bottlesPool, _bottlePrefabs, _bottlesPoolSize);

        SetFirstObjects();

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

    private void SetFirstObjects()
    {
        for (int i = 1; i < _amountOfStreetSegmentsAtStart; i++)
            GetObjectFromPool(_streetSegmentsPool).transform.position = new Vector3(_spawnPositionX + _distBetweenStreetSegmentsAtStart*i, 0, 0);
        
        _lastStreetSegment = GetObjectFromPool(_streetSegmentsPool).transform;
        _lastTrashcan = _trashCansPool.Peek().transform;
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
        WaitForSeconds spawnTimerWait = new WaitForSeconds(SpawnTimerDelay);

        while (true)
        {
            if (_lastStreetSegment.position.x > _spawnPositionX + _streetSegmentClosestSpawnDist && Random.Range(0, 100) > _streetSegmentSpawnProbability)
                _lastStreetSegment = SpawnObjectFromPool(_streetSegmentsPool, 0);

            if (_initialTrashcanSpawnTimer > 0)
                _initialTrashcanSpawnTimer--;
            else if (_lastTrashcan.position.x > _spawnPositionX + _trashcanClosestSpawnDist && Random.Range(0, 100) > _trashcanSpawnProbability)
            {
                _lastTrashcan = SpawnObjectFromPool(_trashCansPool, _trashcanSpawnerZPosition);
                if(_lastTrashcan.transform.position.z > 0)
                    _lastTrashcan.GetComponent<GarbageContainer>().Init(Direction.Right);
                else
                    _lastTrashcan.GetComponent<GarbageContainer>().Init(Direction.Left);
            }

            yield return spawnTimerWait;
        }
    }

    private Transform SpawnObjectFromPool(Queue<GameObject> pool, int ZPosition)
    {
        Transform obj = GetObjectFromPool(pool).transform;
        obj.position = new Vector3(_spawnPositionX, 0, -1 * ZPosition + (2 * Random.Range(0, 2)) * ZPosition);
        return obj;
    }

    private int ChangeSpawnDelay(int baseSpawnDelay, int maxSpawnDelay)
    {
        return Random.Range(baseSpawnDelay, maxSpawnDelay + 1);
    }
}
