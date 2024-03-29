﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Building, BuildingInterface
{

    private GameObject GameObject;

    //Static variables
    private static int cost = 500;
    private static int Health = 50;

    private float time = 0;
    private int BulletSpeed = 30;
    private float ROF = 10;
    private int range = 20;
    private bool Loaded = true;
    SimpleTimer timer;

    //Constructor for the tower
    public Minigun(GameObject gameObject)
    {
        GameObject = gameObject;
        timer = new SimpleTimer(1 / ROF * 1000, true);
    }

    //Called once per frame
    private void Update()
    {

        BuildingScript.BuildingDictionary[gameObject].LoadGun(gameObject);

    }

    //Fires a bullet at the closest zombie
    private void FireBullet(GameObject gameObject)
    {

        GameObject bullet = Resources.Load<GameObject>("Objects/Bullet"); ;
        Rigidbody2D rb;
        Vector3 pos = gameObject.transform.GetChild(1).transform.position;
        Vector3 pos2 = gameObject.transform.GetChild(2).transform.position;

        if (FindZombie(gameObject) != null)
        {
            GameObject targetGO = FindZombie(gameObject);
            bullet.GetComponent<Projectiles>().target = targetGO;
            Vector3 target = FindZombie(gameObject).transform.position;

            gameObject.transform.localRotation = Functions.LookAt(gameObject.transform.position, target);

            if (Vector3.Distance(pos, target) < range)
            {
                bullet = Instantiate(bullet);
                rb = bullet.GetComponent<Rigidbody2D>();
                bullet.transform.position = new Vector3(pos.x, pos.y, pos.z - 0.2f);
                bullet.transform.localRotation = Functions.LookAt(pos, target);
                Vector3 dir = (target - bullet.transform.position).normalized * BulletSpeed;
                rb.velocity = dir;
                bullet = Instantiate(bullet);
                rb = bullet.GetComponent<Rigidbody2D>();
                bullet.transform.position = new Vector3(pos2.x, pos2.y, pos2.z - 0.2f);
                bullet.transform.localRotation = Functions.LookAt(pos2, target);
                rb.velocity = dir;
            }
        }

    }

    //Finds the closest zombie to the turret
    private GameObject FindZombie(GameObject gameObject)
    {
        Vector3 pos = gameObject.transform.position;
        GameObject zombie = null;

        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        if (zombies.Length > 0)
        {
            zombie = zombies[0];
            float dist = Vector3.Distance(pos, zombie.transform.position);
            foreach (GameObject z in zombies)
            {
                float newDist = Vector3.Distance(pos, z.transform.position);

                if (newDist < dist)
                {
                    dist = newDist;
                    zombie = z;
                }
            }
        }

        return zombie;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Zombie")
        {
            if (Functions.CalculateDamage(gameObject, collision.gameObject))
            {
                Destroy(gameObject);
            }

            Destroy(collision.gameObject);
        }
    }

    //-------------------Building Interface Functions----------------//

    //Fire's a bullet at the rate of fire
    void BuildingInterface.LoadGun(GameObject gameObject)
    {
        if (timer.Finished == true)
        {
            FireBullet(gameObject);
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
