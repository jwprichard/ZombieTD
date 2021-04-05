using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BuildingInterface
{
    int cost { get; }
    int Health { get; set; }
    void Step();
}


public class BuildingScript : MonoBehaviour
{
    public static Dictionary<GameObject, BuildingInterface> BuildingDictionary = new Dictionary<GameObject, BuildingInterface>();
    static int idNum = 0;
    //static bool BaseBuilt = false;

    public static void CreateBuilding(string type, Vector3 position)
    {

        try
        {
            idNum++;
            BuildingInterface building;
            GameObject gameObject;// = new GameObject();
            gameObject = Resources.Load<GameObject>("Objects/" + type);
            gameObject = Instantiate(gameObject);
            gameObject.name = type.ToString() + idNum;
            building = GetBuilding(type, gameObject);
            if (building.cost > GameController.GetMoney())
            {
                Debug.Log($"Building: {type}, Cost: {building.cost}");
                throw new System.Exception("Not Enough Money!");
            }
            GameController.AdjustMoney(-building.cost);
            BuildingDictionary.Add(gameObject, building);

            Vector3 newPos = new Vector3(position.x, position.y, 0.2f);

            gameObject.transform.position = newPos;
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
      
    }

    //Return the Building Interface from the given string name
    public static BuildingInterface GetBuilding (string building, GameObject gameObject)
    {
        BuildingInterface Building;

        switch (building)
        {
            case "Base":

                {
                    Building = new Base(gameObject);
                    //BaseBuilt = true;
                }
                break;
            case "Mine":
                Building = new Mine(gameObject);
                break;
            case "Turret":
                Building = new Turret(gameObject);
                break;
            case "Minigun":
                Building = new Minigun(gameObject);
                break;
            case "ArrowTower":
                Building = new ArrowTower(gameObject);
                break;
            default:
                throw new System.Exception($"Building '{building}' not found!");

        }
        return Building;
    }
}

public abstract class Building : MonoBehaviour
{
    //asdfadsf
}
