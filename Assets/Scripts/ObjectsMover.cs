using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMover : MonoBehaviour
{

    private LevelGenerator _levelGenerator;
    private Player _playerController;

    [SerializeField]
    private float _maxObjectsSpeed, _maxEnemySpeed, _minObjectsSpeed, _drinkToSpeedTranslateMultiplier, _maxSpeedModifier, _minSpeedModifier;

    private float _objectsSpeed, _enemySpeed;


    [SerializeField]
    private Transform _objectsDestroyPoint, _enemy;

    [SerializeField]
    private AnimationCurve _drinkSpeedCurve;

    private const float SpeedUpdateDelay = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        _levelGenerator = FindObjectOfType<LevelGenerator>();
        _playerController = FindObjectOfType<Player>();
        StartCoroutine("SpeedUpdateCoroutine");

        _objectsSpeed = _maxObjectsSpeed;
        _enemySpeed = _maxEnemySpeed;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < _levelGenerator.ActiveMovingObjects.Count; i++)
        {
            Transform obj = _levelGenerator.ActiveMovingObjects[i].transform;
            obj.Translate(_objectsSpeed * Time.deltaTime, 0, 0, Space.World);
            if (obj.position.x > _objectsDestroyPoint.position.x)
                _levelGenerator.ReturnObjectToPool(obj.gameObject);
        }

        _enemy.transform.Translate(_enemySpeed * Time.deltaTime, 0, 0);
    }

    private IEnumerator SpeedUpdateCoroutine()
    {
        WaitForSeconds speedUpdateWait = new WaitForSeconds(SpeedUpdateDelay);
        float drinkValue = 0;
        float speedModifier = 0;

        while (true)
        {
            drinkValue = _playerController.Drink;
            speedModifier = _drinkSpeedCurve.Evaluate(drinkValue);

            _objectsSpeed = _maxObjectsSpeed * speedModifier;
            _enemySpeed = _maxEnemySpeed * (1 - speedModifier);

            yield return speedUpdateWait;            
        }
    }

}
