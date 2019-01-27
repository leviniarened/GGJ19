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
    private Direction containerDirection;

    [SerializeField]
    private Transform spawnPosObject;

    [SerializeField]
    private Animator animatorController;

    [SerializeField]
    private float rangeToKickSuccessfully = 1;

    private Player player;

    public static event Action OnKickFail;

    LevelGenerator levelGenerator;

    bool used = false;

    //Init here
    private void OnEnable()
    {
        if (animatorController != null)
            animatorController.Play("Idle");
        used = false;
    }

    private void OnDisable()
    {
        //if (animatorController != null)
        //    animatorController.Play("Idle");
    }


    /// <summary>
    /// On press kick container
    /// </summary>
    public void KickContainer(Direction kickDirection)
    {
        if (Vector3.Distance(player.transform.position, transform.position) >= rangeToKickSuccessfully)
        {
            //TODO player fail with kick handle event
            player.PlayKickFail(kickDirection);

            OnKickFail?.Invoke();
            return;//can't kick object
        }

        if(kickDirection != containerDirection)
        {
            player.PlayKickFail(kickDirection);

            OnKickFail?.Invoke();
            return;
        }

        if(used)
        {
            player.PlayKickFail(kickDirection);
            return;
        }

        if(animatorController!=null)
            animatorController.Play("Drop");

        var bottle = levelGenerator.GetBottle().GetComponent<Bonus>();
        bottle.InitBonusDirection(containerDirection);
        bottle.transform.position = spawnPosObject.transform.position;
        bottle.Force();
        used = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeToKickSuccessfully);
    }

    public void Init(Direction dir)
    {
        containerDirection = dir;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }
}
