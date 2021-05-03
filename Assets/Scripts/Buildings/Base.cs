using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IBuilding
{


    private GameObject GameObject;

    private static int Cost = 0;
    private static int Health = 100;
    private int Range = 0;

    public Base(GameObject gameObject)
    {
        GameObject = gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Zombie")
        {
            Debug.Log("here");
            if (Functions.CalculateDamage(gameObject, collision.gameObject))
            {
                Destroy(gameObject);
                GameController.gameOver = true;
            }

            Destroy(collision.gameObject);
        }
    }



    //------------Building Interface Functions---------------//
    int[] IBuilding.GetStats()
    {
        int[] stats = new int[2];
        stats[0] = Health;
        stats[1] = Range;

        return stats;
    }
    int IBuilding.cost
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
