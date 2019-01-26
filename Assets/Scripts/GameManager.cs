using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    private Transform _enemy;
    private const float GameoverConditionCheckDelay = 0.2f;
    [SerializeField]
    private float _distToEnemyForVictory;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        StartCoroutine("GameoverCheck");
    }

    private IEnumerator GameoverCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(GameoverConditionCheckDelay);
        while (true)
        {
            if (Mathf.Abs(_player.transform.position.x - _enemy.position.x) < _distToEnemyForVictory)
            {
                Debug.Log("Враг пойман!!!! гамовер, виктори и т.п.");
            }

            yield return wait;
        }
    }


    

}
