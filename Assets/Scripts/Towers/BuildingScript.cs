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
        GameObject gameObject = Resources.Load<GameObject>("Objects/Buildings/" + type + "Preview");
        gameObject = Instantiate(gameObject);
        gameObject.name = type;
        gameObject.tag = "Preview";
        return gameObject;
    }

    public static void CreateBuilding(string type, Vector3 pos)
    {

        try
        {
            idNum++;
            IBuilding building;
            GameObject go = Resources.Load<GameObject>("Objects/Buildings/" + type);
            go = Instantiate(go);
            go.name = type + idNum;
            building = GetBuilding(type, go);
            if (building.cost > GameController.GetMoney())
            {
                Debug.Log($"Building: {go.name}, Cost: {building.cost}");
                Destroy(go);
                throw new System.Exception("Not Enough Money!");
            }

            GameController.AdjustMoney(-building.cost);
            BuildingDictionary.Add(go, building);

            go.transform.position = pos;
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
                    Building = gameObject.AddComponent<Base>();
                    //BaseBuilt = true;
                }
                break;
            case "Mine":
                Building = gameObject.AddComponent<Mine>();
                break;
            case "Turret":
                Building = gameObject.AddComponent<Turret>();
                break;
            case "Minigun":
                Building = gameObject.AddComponent<Minigun>();
                break;
            case "ArrowTower":
                Building = gameObject.AddComponent<ArrowTower>();
                break;
            default:
                throw new System.Exception($"Building '{building}' not found!");

        }
        return Building;
    }
}
