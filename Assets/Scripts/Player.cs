using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public float Drink
    {
        get { return drink; }
        set
        {
            drink = value;
            ui.DrinkIndicator.fillAmount = drink;
        }
    }

    float drink = 0.5f;//ranged from 0 to 1
    [SerializeField]
    float drinkDecreaseParam = 0.1f;
    UIController ui;

    public void PickUpDrink(float v)
    {
        Drink += v;
    }

    public void PlayPickUpSuccess(BonusType bonus, Direction pickupDirection)
    {
        Debug.Log("Pickup success " + bonus.ToString() + " " + pickupDirection.ToString());
    }

    public void PlayPickUpFail(Direction pickupDirection)
    {
        Debug.Log("Pickup fail " + pickupDirection.ToString());
    }

    public void PlayKickSuccess(Direction kickDirection)
    {
        Debug.Log("Kick success " + kickDirection.ToString());
    }

    public void PlayKickFail(Direction kickDirection)
    {
        Debug.Log("Kick fail " + kickDirection.ToString());
    }

    void Start()
    {
        ui = FindObjectOfType<UIController>();
        InputController.LeftKick += InputController_LeftKick;
        InputController.RightKick += InputController_RightKick;
        InputController.GrabLeft += InputController_GrabLeft;
        InputController.GrabRight += InputController_GrabRight;
    }

    private void InputController_GrabRight()
    {
        var nearestBonus = GetNearestBonus();
        if (nearestBonus == null)
            return;

        nearestBonus.PickUpBonus(Direction.Right);
    }

    private void InputController_GrabLeft()
    {
        var nearestBonus = GetNearestBonus();
        if (nearestBonus == null)
            return;

        nearestBonus.PickUpBonus(Direction.Left);
    }

    private void InputController_RightKick()
    {
        var nearestContainer = GetNearestContainer();
        if (nearestContainer == null) return;

        nearestContainer.KickContainer(Direction.Right);
    }

    private void InputController_LeftKick()
    {
        var nearestContainer = GetNearestContainer();
        if (nearestContainer == null) return;

        nearestContainer.KickContainer(Direction.Left);
    }

    GarbageContainer GetNearestContainer()
    {
        var garbageC = FindObjectsOfType<GarbageContainer>().Select(t => t.transform);
        return GetClosest(garbageC).gameObject.GetComponent<GarbageContainer>();
    }

    Bonus GetNearestBonus()
    {
        var garbageC = FindObjectsOfType<Bonus>().Select(t => t.transform);
        return GetClosest(garbageC).gameObject.GetComponent<Bonus>();
    }

    Transform GetClosest(IEnumerable<Transform> obj)
    {
        if (obj == null)
            return null;

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in obj)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    void Update()
    {
        Drink = Mathf.Clamp01(Drink - Time.deltaTime * drinkDecreaseParam);
    }
}
