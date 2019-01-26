using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player _player;
    private Transform _enemy;
    private const float GameoverConditionCheckDelay = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }


    

}
