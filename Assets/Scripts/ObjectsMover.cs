using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMover : MonoBehaviour
{

    private LevelGenerator _levelGenerator;

    public int ObjectsSpeed;
    public int EnemySpeed;
    public float SpeedMultiplier = 1;
    [SerializeField]
    private float _speedMultiplierDecreaseAmount;
    [SerializeField]
    private int _speedDecreaseDelay;
    WaitForSeconds _speedDecreaseWait;

    [SerializeField]
    private Transform _objectsDestroyPoint, _enemy;

    private int _maxSpeedMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        _levelGenerator = FindObjectOfType<LevelGenerator>();
        _speedDecreaseWait = new WaitForSeconds(_speedDecreaseDelay);
        _maxSpeedMultiplier = (int)SpeedMultiplier;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < _levelGenerator.ActiveMovingObjects.Count; i++)
        {
            Transform obj = _levelGenerator.ActiveMovingObjects[i].transform;
            obj.Translate(SpeedMultiplier * ObjectsSpeed * Time.deltaTime, 0, 0);
            if (obj.position.x > _objectsDestroyPoint.position.x)
                _levelGenerator.ReturnObjectToPool(obj.gameObject);
        }

        _enemy.transform.Translate(SpeedMultiplier * EnemySpeed * Time.deltaTime, 0, 0);
    }

    private IEnumerator SpeedDecreaseCoroutine()
    {
        while(true)
        {
            yield return _speedDecreaseWait;
            SpeedMultiplier -= _speedMultiplierDecreaseAmount;
        }
    }
}
