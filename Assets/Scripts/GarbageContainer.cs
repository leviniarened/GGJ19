using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left, Right
}

public class GarbageContainer : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosObject;

    [SerializeField]
    private Animator animatorController;

    [SerializeField]
    private float rangeToKickSuccessfully = 1;

    private Transform player;

    public static event Action OnKickFail;

    LevelGenerator levelGenerator;

    //Init here
    private void OnEnable()
    {
        if (animatorController != null)
            animatorController.Play("Idle");
    }

    private void OnDisable()
    {
        if (animatorController != null)
            animatorController.Play("Idle");
    }


    /// <summary>
    /// On press kick container
    /// </summary>
    public void KickContainer()
    {
        if (Vector3.Distance(player.position, transform.position) >= rangeToKickSuccessfully)
        {
            //TODO player fail with kick handle event
            OnKickFail?.Invoke();
            return;//can't kick object
        }
        if(animatorController!=null)
            animatorController.Play("Drop");
        //levelGenerator.
        //var bonus = Instantiate(bonusPrefabs[randomBonusIndex]);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            KickContainer();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeToKickSuccessfully);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }
}
