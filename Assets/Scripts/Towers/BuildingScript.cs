using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    int cost { get; }
    int Health { get; set; }
    void Step();
}


public class BuildingScript : MonoBehaviour
{
    public static Dictionary<GameObject, IBuilding> BuildingDictionary = new Dictionary<GameObject, IBuilding>();
    static int idNum = 0;
    //static bool BaseBuilt = false;

    public static GameObject BuildingPreview(string type)
    {
        GameObject gameObject = Resources.Load<GameObject>("Objects/" + type);
        gameObject = Instantiate(gameObject);
        gameObject.name = type.ToString() + idNum;
        return gameObject;
    }

    public static void CreateBuilding(GameObject gameObject, Vector3 position)
    {

        try
        {
            idNum++;
            IBuilding building;
            //GameObject gameObject;// = new GameObject();
            //gameObject = Resources.Load<GameObject>("Objects/" + type);
            //gameObject = Instantiate(gameObject);
            //gameObject.name = type.ToString() + idNum;
            building = GetBuilding(gameObject.name, gameObject);
            if (building.cost > GameController.GetMoney())
            {
                Debug.Log($"Building: {gameObject.name}, Cost: {building.cost}");
                Destroy(gameObject);
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
    public static IBuilding GetBuilding (string building, GameObject gameObject)
    {
        IBuilding Building;

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
