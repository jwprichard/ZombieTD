using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, IBuilding
{

    //private GameObject GameObject;

    private int Cost = 100;
    private int Health = 1;
    private int Range = 0;

    private float ROF = 1;
    SimpleTimer timer;

    //Contructor for the Mine
    public Mine()
    {
        timer = new SimpleTimer(1 / ROF * 1000, true);
    }

    //Called once per frame
    private void Update()
    {
        CheckTimer();
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
    int[] IBuilding.GetStats()
    {
        int[] stats = new int[2];
        stats[0] = Health;
        stats[1] = Range;

        return stats;
    }
    //Return the cost of the Building
    int IBuilding.Cost
    {
        get
        {
            return Cost;
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
