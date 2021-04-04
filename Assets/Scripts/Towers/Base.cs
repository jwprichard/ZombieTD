using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, BuildingInterface
{


    private GameObject GameObject;

    private static int cost = 0;
    private static int Health = 100;

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
    void BuildingInterface.Step()
    {

    }
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
