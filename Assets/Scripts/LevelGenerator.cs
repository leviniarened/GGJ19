using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
        PopulatePool(pool, prefabs[Random.Range(0, prefabs.Length)], poolSize);
    }

    private GameObject GetObjectFromPool(Queue<GameObject> pool)
    {
        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    private void ReturnObjectToPool(GameObject obj, Queue<GameObject> pool)
    {
        pool.Enqueue(obj);
        obj.SetActive(false);
    }

    public void DestroyTrashCan(GameObject trashcan)
    {
        ReturnObjectToPool(trashcan, _trashCansPool);
    }
}
