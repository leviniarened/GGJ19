using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event Action LeftKick;
    public static event Action RightKick;
    public static event Action GrabLeft;
    public static event Action GrabRight;

    [SerializeField]
    KeyCode leftKickKey = KeyCode.A;
    [SerializeField]
    KeyCode rightKickKey = KeyCode.D;
    [SerializeField]
    KeyCode leftGrabKey = KeyCode.Q;
    [SerializeField]
    KeyCode rightGrabKey = KeyCode.E;


    void Start()
    {
        var ui = FindObjectOfType<UIController>();
        ui.LeftKickButton.onClick.AddListener(LeftKickCall);
        ui.RightKickButton.onClick.AddListener(RightKickCall);
        ui.LeftGrabButton.onClick.AddListener(GrabLeftCall);
        ui.RightGrabButton.onClick.AddListener(GrabRightCall);
    }

    void LeftKickCall()
    {
        LeftKick?.Invoke();
    }

    void RightKickCall()
    {
        RightKick?.Invoke();
    }

    void GrabLeftCall()
    {
        GrabLeft?.Invoke();
    }

    void GrabRightCall()
    {
        GrabRight?.Invoke();
    }

    void Update()
    {
        if(Input.GetKeyDown(leftKickKey))
        {
            LeftKickCall();
        }
        else if (Input.GetKeyDown(rightKickKey))
        {
            RightKickCall();
        }
        else if (Input.GetKeyDown(leftGrabKey))
        {
            GrabLeftCall();
        }
        else if (Input.GetKeyDown(rightGrabKey))
        {
            GrabRightCall();
        }
    }
}
