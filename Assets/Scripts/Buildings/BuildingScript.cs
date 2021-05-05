using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public static void CreateBuilding(string type, Tile tile)
    {

        try
        {
            idNum++;
            IBuilding building;
            GameObject go = Resources.Load<GameObject>("Objects/Buildings/" + type);
            go = Instantiate(go);
            go.name = type + idNum;
            building = GetBuilding(type, go);
            if (building.Cost > GameController.GetMoney())
            {
                Debug.Log($"Building: {go.name}, Cost: {building.Cost}");
                Destroy(go);
                throw new System.Exception("Not Enough Money!");
            }

            GameController.AdjustMoney(-building.Cost);
            BuildingDictionary.Add(go, building);

            tile.SetBuilding(building);

            go.transform.position = tile.transform.position;
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
      
    }

    //Return the Building Interface from the given string name
    public static IBuilding GetBuilding (string building, GameObject gameObject)
    {
        IBuilding Building = building switch
        {
            "Base" => gameObject.AddComponent<Base>(),
            "Mine" => gameObject.AddComponent<Mine>(),
            "Turret" => gameObject.AddComponent<BulletTurret>(),
            "Minigun" => gameObject.AddComponent<Minigun>(),
            "ArrowTower" => gameObject.AddComponent<ArrowTower>(),
            _ => throw new System.Exception($"Building '{building}' not found!"),
        };
        return Building;
    }
}
