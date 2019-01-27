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

    public delegate void GameOverEvent();
    public static GameOverEvent GameOverVictory, GameOverLoss;

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
                if (GameOverVictory != null)
                    GameOverVictory.Invoke();
            }

            else if (_player.Drink == 0)
            {
                if (GameOverLoss != null)
                    GameOverLoss.Invoke();
            }


            yield return wait;
        }
    }
    
}
