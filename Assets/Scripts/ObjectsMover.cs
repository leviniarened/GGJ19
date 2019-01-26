using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMover : MonoBehaviour
{

    private LevelGenerator _levelGenerator;

    public int ObjectsSpeed;
    public int EnemySpeed;

    [SerializeField]
    private Transform _objectsDestroyPoint, _enemy;


    // Start is called before the first frame update
    void Start()
    {
        _levelGenerator = FindObjectOfType<LevelGenerator>();
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < _levelGenerator.ActiveMovingObjects.Count; i++)
        {
            Transform obj = _levelGenerator.ActiveMovingObjects[i].transform;
            obj.Translate(ObjectsSpeed * Time.deltaTime, 0, 0);
            if (obj.position.x > _objectsDestroyPoint.position.x)
                _levelGenerator.ReturnObjectToPool(obj.gameObject);
        }

        _enemy.transform.Translate(EnemySpeed * Time.deltaTime, 0, 0);
    }
}
