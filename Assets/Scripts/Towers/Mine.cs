using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, IBuilding
{

    private GameObject GameObject;

    private int cost = 100;
    private int Health = 1;
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

        BuildingScript.BuildingDictionary[gameObject].Step();

    }

    private void CheckTimer()
    {
        if (timer.Finished == true)
        {
            AddMoney();
            timer = new SimpleTimer(1 / ROF * 1000, false);
        }
    }

    //Add money to the players count
    private void AddMoney()
    {
        GameController.AdjustMoney(50);
    }

    //-------------------Building Interface Functions----------------//
    void IBuilding.Step()
    {
        CheckTimer();
    }
    //Return the cost of the Building
    int IBuilding.cost
    {
        get
        {
            return cost;
        }
    }

    int IBuilding.Health
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
