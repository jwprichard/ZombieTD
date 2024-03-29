﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Zombie : MonoBehaviour, ZombieInterface
{

    private GameObject GameObject;
    private Rigidbody2D rb;
    private static int MoveSpeed = 5;

    int Health = 20;
    const int Damage = 50;
    const int Cost = 5;

    private static int value = 50;

    //Instantiates the Zombie1 class
    public Slow_Zombie(GameObject gameObject)
    {
        GameObject = gameObject;
        rb = GameObject.GetComponent<Rigidbody2D>();
    }
    
    //Called once per frame
    void Update()
    {
        ZombieScript.ZombieDictionary[gameObject].move(gameObject);
    }

    ////Finds the closest tower to the zombie
    private GameObject FindBuilding(GameObject gameObject)
    {
        Vector3 position = gameObject.transform.position;
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

        if (buildings.Length > 0)
        {
            GameObject building = null;
            float dist = int.MaxValue;

            foreach (GameObject b in buildings)
            {

                float newDist = Vector3.Distance(position, b.transform.position);

                if (newDist < dist)
                {
                    dist = newDist;
                    building = b;
                }
            }
            return building;
        }
        //If no buildings were found
        return null;

    }

    //On collision witht he trigger object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            if (Functions.CalculateDamage(gameObject, collision.gameObject))
            {
                Destroy(gameObject);
                GameController.AddMoney(value);
            }

            Destroy(collision.gameObject);
        }
    }

    //--------------Interface Functions-------------------//

    //From interface to return health
    int ZombieInterface.Health
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

    int ZombieInterface.Damage
    {
        get
        {
            return Damage;
        }
    }

    int ZombieInterface.Cost
    {
        get
        {
            return Cost;
        }
    }

    //Move to the closest tower
    void ZombieInterface.move(GameObject gameObject)
    {
        if (FindBuilding(gameObject) != null)
        {
            Vector3 target = FindBuilding(gameObject).transform.position;
            Vector3 dir = (target - gameObject.transform.position).normalized * MoveSpeed;
            rb.velocity = dir;
            gameObject.transform.localRotation = Functions.LookAt(gameObject.transform.position, target);

        }
    }

}
