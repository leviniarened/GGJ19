using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

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

        GameOverVictory += Victory;
    }

    private IEnumerator GameoverCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(GameoverConditionCheckDelay);
        while (true)
        {

            if (_player.transform.position.x - _enemy.position.x < _distToEnemyForVictory)
                GameOverVictory?.Invoke();

            else if (_player.Drink == 0 )
                GameOverLossDrinkZero?.Invoke();

            else if (_player.Drink == 1)
                    GameOverLossDrinkTooMuch?.Invoke();

            yield return wait;
        }
    }

    private void Victory()
    {
        SceneManager.LoadScene(2);
    }
    
}
