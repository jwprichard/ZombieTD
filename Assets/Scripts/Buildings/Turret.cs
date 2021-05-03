using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IBuilding
{

    //private GameObject GameObject;

    private static int Cost = 250;
    private static int Health = 100;
    private int Range = 50;

    private int BulletSpeed = 30;
    private float ROF = 1;
    SimpleTimer timer;

    //Constructor for the tower
    public Turret()
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
            FireBullet();
            timer = new SimpleTimer(1 / ROF * 1000, false);
        }
    }

    //Fire a bullet at the closest zombie
    private void FireBullet()
    {

        GameObject bullet = Resources.Load<GameObject>("Objects/Bullet");
        Rigidbody2D rb;
        Vector3 pos = gameObject.transform.GetChild(0).transform.position;

        if (FindZombie(gameObject) != null)
        {
            GameObject targetGO = FindZombie(gameObject);
            bullet.GetComponent<Projectiles>().target = targetGO;
            Vector3 target = FindZombie(gameObject).transform.position;

            gameObject.transform.localRotation = Functions.LookAt(gameObject.transform.position, target);

            if (Vector3.Distance(pos, target) < Range)
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
    int[] IBuilding.GetStats()
    {
        int[] stats = new int[2];
        stats[0] = Health;
        stats[1] = Range;

        return stats;
    }

    //Return the cost of the Building
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
