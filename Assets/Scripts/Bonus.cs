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
    private float pickupDistance;


    public BonusType thisBonusType;
    [SerializeField]
    private Vector3 forceDirection;
    private Rigidbody rb;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupDistance);
    }



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    [ContextMenu("Force")]
    public void Force()
    {
        rb.AddForce(forceDirection, ForceMode.Impulse);
    }

}
