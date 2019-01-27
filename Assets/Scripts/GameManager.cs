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
                GameoverVictory();
            else if (_player.Drink == 0)
                GameoverLoss();           

            yield return wait;
        }
    }

    private void GameoverVictory()
    {
        Debug.Log("Враг пойман!!!! гамовер, виктори и т.п.");
        GameOverVictory.Invoke();
    }


    private void GameoverLoss()
    {
        Debug.Log("Вам не хватило бухла, вы проиграли.");
        GameOverLoss.Invoke();
    }

}
