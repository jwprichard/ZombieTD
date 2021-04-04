using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BuildingInterface
{
    int cost { get; }
    int Health { get; set; }
    void LoadGun(GameObject gameObject);
}


public class BuildingScript : MonoBehaviour
{
    public static Dictionary<GameObject, BuildingInterface> BuildingDictionary = new Dictionary<GameObject, BuildingInterface>();
    static int idNum = 0;
    static bool BaseBuilt = false;

    public static GameObject CreateBuilding(BuildingType type, Vector3 position)
    {
        idNum++;
        BuildingInterface building;
        GameObject gameObject;// = new GameObject();
        gameObject = Resources.Load<GameObject>("Objects/"+type);
        gameObject = Instantiate(gameObject);

        switch (type)
        {
            case BuildingType.Base:
                if (!BaseBuilt)
                {
                    building = new Base(gameObject);
                    BaseBuilt = true;
                }
                else
                {
                    Destroy(gameObject);
                    return null;
                }
                break;
            case BuildingType.Mine:
                building = new Mine(gameObject);
                break;
            case BuildingType.Turret:
                building = new Turret(gameObject);
                break;
            case BuildingType.Minigun:
                building = new Minigun(gameObject);
                break;
            case BuildingType.ArrowTower:
                building = new ArrowTower(gameObject);
                break;
            default:
                return null;
                
        }

        gameObject.name = type.ToString() + idNum;
        BuildingDictionary.Add(gameObject, building);

        Vector3 newPos = new Vector3(position.x, position.y, 0.2f);

        gameObject.transform.position = newPos;
        return gameObject;
       
    }
}

public abstract class Building : MonoBehaviour
{
    //asdfadsf
}

public enum BuildingType
{
    Base = 0,
    Mine = 100,
    Turret = 250,
    Minigun = 750,
    ArrowTower = 500,
    Laser = 1000
}
