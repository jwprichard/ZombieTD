using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IBuilding
{

    private GameObject GameObject;

    private static int cost = 250;
    private static int Health = 50;

    private float time = 0;
    private int BulletSpeed = 30;
    private float ROF = 1;
    private int range = 50;
    private bool Loaded = true;
    SimpleTimer timer;

    //Constructor for the tower
    public Turret(GameObject gameObject)
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
            FireBullet();
            timer = new SimpleTimer(1 / ROF * 1000, false);
        }
    }

    //Fire a bullet at the closest zombie
    private void FireBullet()
    {

        GameObject bullet = Resources.Load<GameObject>("Objects/Bullet");
        Rigidbody2D rb;
        Vector3 pos = GameObject.transform.GetChild(1).transform.position;

        if (FindZombie(GameObject) != null)
        {
            GameObject targetGO = FindZombie(GameObject);
            bullet.GetComponent<Projectiles>().target = targetGO;
            Vector3 target = FindZombie(GameObject).transform.position;

            GameObject.transform.localRotation = Functions.LookAt(GameObject.transform.position, target);

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
