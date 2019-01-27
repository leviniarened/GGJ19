using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMover : MonoBehaviour
{

    private LevelGenerator _levelGenerator;
    private Player _playerController;

    [SerializeField]
    private float _baseObjectsSpeed, _baseEnemySpeed;

    private float _objectsSpeed, _enemySpeed;


    [SerializeField]
    private Transform _objectsDestroyPoint, _enemy;

    [SerializeField]
    private AnimationCurve _drinkSpeedCurve;

    [SerializeField]
    private float _enemySpeedModifier;

    private const float SpeedUpdateDelay = 0.1f;




    // Start is called before the first frame update
    void Start()
    {
        _levelGenerator = FindObjectOfType<LevelGenerator>();
        _playerController = FindObjectOfType<Player>();
        StartCoroutine("SpeedUpdateCoroutine");

        _objectsSpeed = _baseObjectsSpeed;
        _enemySpeed = _baseEnemySpeed;
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

            //_objectsspeed = _baseobjectsspeed * speedmodifier;
            //_enemyspeed = _baseenemyspeed * (speedmodifier - _enemyspeedmodifier);

            _objectsSpeed = _baseObjectsSpeed + speedModifier;
            _enemySpeed = _baseEnemySpeed + speedModifier;

            yield return speedUpdateWait;
        }
    }

}
