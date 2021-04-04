using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : MonoBehaviour, BuildingInterface
{

    private GameObject GameObject;

    //Static Variables
    private static int cost = 250;
    private static int Health = 100;

    private int BulletSpeed = 30;
    private float ROF = 0.5f;
    private int range = 100;
    private SimpleTimer timer;

    //Constructor for the tower
    public ArrowTower(GameObject gameObject)
    {
        GameObject = gameObject;
        timer = new SimpleTimer(1 / ROF * 1000, false);
        Debug.Log(timer);
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
            FireBullet();
            timer = new SimpleTimer(1 / ROF * 1000, false);
        }
    }

    //Fire a bullet at the closest zombie
    private void FireBullet()
    {

        GameObject bullet = Resources.Load<GameObject>("Objects/Arrow"); ;
        Rigidbody2D rb;
        Vector3 pos = gameObject.transform.GetChild(1).transform.position;

        if (FindZombie(gameObject) != null)
        {
            bullet.GetComponent<Arrow>().turret = gameObject;
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

    //On collision with tower
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
    void BuildingInterface.Step()
    {

    }

    //Return the cost of the Building
    int BuildingInterface.cost
    {
        get
        {
            return cost;
        }
    }

    //Return the health of he Building
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
