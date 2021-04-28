using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ZombieInterface
{
    int Health { get; set; }
    int Damage { get; }
    int Cost { get; }
    void move(GameObject gameObject);
}

public class ZombieScript : MonoBehaviour
{
    public static Dictionary<GameObject, ZombieInterface> ZombieDictionary = new Dictionary<GameObject, ZombieInterface>();
    static int idNum = 0;

    public static GameObject CreateZombie(ZombieType type, Vector3 position)
    {
        idNum++;
        ZombieInterface zombie;
        GameObject gameObject;
        gameObject = Resources.Load<GameObject>("Objects/" + type);
        gameObject = Instantiate(gameObject);

        

        switch (type)
        {
            case ZombieType.Slow_Zombie:
                zombie = new Slow_Zombie(gameObject);
                break;
            case ZombieType.Fast_Zombie:
                zombie = new Fast_Zombie(gameObject);
                break;
            case ZombieType.Zombie3:
                zombie = new Zombie3(gameObject);
                break;
            default:
                return null;

        }
        gameObject.name = type.ToString() + idNum;
        ZombieDictionary.Add(gameObject, zombie);


        Vector3 newPos = new Vector3(position.x, position.y, 0);

        gameObject.transform.position = newPos;
        return gameObject;

    }

    public static void CreateMonsterSpawner(Vector3 pos, int Frequency, int Strength, ZombieType[] ZombieTypes)
    {
        GameObject gameObject;
        gameObject = Resources.Load<GameObject>("Objects/Hive");
        gameObject = Instantiate(gameObject);
        gameObject.transform.position = pos;
        MonsterSpawner ms = gameObject.transform.GetComponent<MonsterSpawner>();
        ms.SetValues(Frequency, Strength, ZombieTypes);
        ms.begin();
    }
}

public enum ZombieType
{
    Slow_Zombie = 70,
    Fast_Zombie = 60,
    Zombie3 = 80
}
