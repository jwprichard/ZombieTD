using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Building, BuildingInterface
{

    private GameObject GameObject;

    private static int cost = 100;
    private static int Health = 1;
    private float ROF = 1;
    SimpleTimer timer;

    //Contructor for the Mine
    public Mine(GameObject gameObject)
    {
        GameObject = gameObject;
        timer = new SimpleTimer(1 / ROF * 1000, true);
    }

    //Called once per frame
    private void Update()
    {

        BuildingScript.BuildingDictionary[gameObject].LoadGun(gameObject);

    }

    //The Action made by the tower
    private void Action()
    {
        GameController.AddMoney(50);
    }

    //-------------------Building Interface Functions----------------//

    //Fire's a bullet at the rate of fire
    void BuildingInterface.LoadGun(GameObject gameObject)
    {
        if (timer.Finished == true)
        {
            Action();
            timer = new SimpleTimer(1 / ROF * 1000, false);
        }
    }


    //Return the cost of the Building
    int BuildingInterface.cost
    {
        get
        {
            return cost;
        }
    }

    int BuildingInterface.Health
    {
        get
        {
            return Health;
        }

        set
        {
            Health = value;
        }
    }
}
