using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusType
{
    Alco, NotAlco
}

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private Direction bonusDirection;

    [SerializeField]
    private float pickupDistance;

    public BonusType thisBonusType;
    [SerializeField]
    private Vector3 forceDirection;
    private Rigidbody rb;
    private Player player;

    public static event Action OnPickUpFail;
    public static event Action OnPickUpSuccess;

    LevelGenerator levelGenerator;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupDistance);
    }

    private void Start()
    {        
        player = FindObjectOfType<Player>();
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }

    public void InitBonusDirection(Direction dir)
    {
        bonusDirection = dir;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
    }

    public void PickUpBonus(Direction pickupDirection)
    {
        if (Vector3.Distance(player.transform.position, transform.position) >= pickupDistance)
        {
            //TODO player fail with kick handle event
            OnPickUpFail?.Invoke();
            player.PlayPickUpFail(pickupDirection);
            return;//can't kick object
        }

        if (pickupDirection != bonusDirection)
        {
            player.PlayPickUpFail(pickupDirection);
            OnPickUpFail?.Invoke();
            return;
        }
        var bonusCopy = Instantiate(this.gameObject);
        Destroy(bonusCopy.GetComponent<Bonus>());
        
        player.PlayPickUpSuccess(thisBonusType, bonusDirection, bonusCopy);
        levelGenerator.ReturnObjectToPool(this.gameObject);
    }


    [ContextMenu("Force")]
    public void Force()
    {
        rb.AddForce(forceDirection, ForceMode.Impulse);
    }

}
