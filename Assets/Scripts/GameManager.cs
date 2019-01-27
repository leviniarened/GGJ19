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
    public static GameOverEvent GameOverVictory, GameOverLossDrinkZero, GameOverLossDrinkTooMuch;

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

            else if (_player.Drink == 0 )
            {
                if (GameOverLossDrinkZero != null)
                    GameOverLossDrinkZero.Invoke();
            }

            else if (_player.Drink == 1)
            {
                if (GameOverLossDrinkTooMuch != null)
                    GameOverLossDrinkTooMuch.Invoke();
            }

            yield return wait;
        }
    }
    
}
