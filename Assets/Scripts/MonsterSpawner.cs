using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class MonsterSpawner : MonoBehaviour
{

    private int Frequency;
    private float Strength;
    private ZombieType[] ZombieTypes;
    private SimpleTimer Timer;
    private bool Bool = false;

    public void SetValues(int frequency, int strength, ZombieType[] zombieTypes)
    {
        Frequency = frequency;
        Strength = strength;
        ZombieTypes = zombieTypes;
    }

    public void begin()
    {
        Timer = new SimpleTimer(Frequency * 1000, false);
    }

    private void Update()
    {
        if (Timer.Finished == true)
        {
            spawn(gameObject);
            //Timer.Finished = false;
            if (Strength > 0)
            {
                Timer = new SimpleTimer(500, false);
            }
            else
            {
                Timer = new SimpleTimer(Frequency * 1000, false);
                Strength = 10*(GameController.time /5 );
                Debug.Log(Strength);
            }
        }
    }

    private void spawn(GameObject gameObject)
    {
        {
            int total = 0;
            foreach (ZombieType type in ZombieTypes)
            {
                total += (int)type;
            }
            int number = Random.Range(1, total);

            int spawnPos = Functions.RandomNumber(1, 4);

            Vector3 pos = gameObject.transform.GetChild(spawnPos).transform.position;

            foreach (ZombieType type in ZombieTypes)
            {
                number -= (int)type;
                if (number <= 0)
                {
                    Strength -= ZombieScript.CreateZombie(type, pos).GetComponent<ZombieInterface>().Cost;
                    break;
                }
            }
        }
    }

}
