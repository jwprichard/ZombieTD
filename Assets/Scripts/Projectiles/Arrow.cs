using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, Projectiles
{
    private int damage = 20;
    private static GameObject bullet;
    private static int LifeSpan = 5;
    private static float time = 0;
    public GameObject turret;
    public GameObject target;

    private void Update()
    {
        
    }

    GameObject Projectiles.target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    int Projectiles.GetDamage()
    {
        return damage;
    }
}
