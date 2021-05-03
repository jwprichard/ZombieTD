using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    int Health;
    int Frequency = 10;
    float time = 0;
    ZombieType[] SpawnableZombies;

    public ZombieSpawner()
    {
    }

    private void Update()
    {
        //time += Time.deltaTime;
        //if(time > Frequency)
        //{
        //    Spawn();
        //    time = 0;
        //    Frequency -= 1;
        //}
    }

    //Spawn zombies at specific points
    private void Spawn()
    {
        int type = Random.Range(1, 100);
        int num = Random.Range(1, 5);

        if (type < 70)
        {
            ZombieType zt = ZombieType.Fast_Zombie;
            for (int i = 0; i < num; i++)
            {
                ZombieScript.CreateZombie(zt, gameObject.transform.position);
            }
        }
        else
        {
            ZombieType zt = ZombieType.Slow_Zombie;
            for (int i = 0; i < num; i++)
            {
                ZombieScript.CreateZombie(zt, gameObject.transform.position);
            }
        }

    }
}
