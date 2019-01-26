using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        ui = FindObjectOfType<UIController>();
    }

    void Update()
    {
        Drink = Mathf.Clamp01(Drink - Time.deltaTime * drinkDecreaseParam);
    }
}
