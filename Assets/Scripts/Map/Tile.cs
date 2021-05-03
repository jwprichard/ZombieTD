using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private static Dictionary<Vector3, Tile> TileDictionary = new Dictionary<Vector3, Tile>();
    static int idNum = 0;

    public string Type { get; set; }
    private IBuilding Building { get; set; }

    public void SetBuilding(IBuilding building)
    {
        Building = building;
    }

    public IBuilding GetBuilding()
    {
        return Building;
    }

    public static GameObject CreateTile(string type, int[] location, GameObject parent)
    {
        idNum++;
        GameObject gameObject = Resources.Load<GameObject>("Objects/" + type);
        gameObject = Instantiate(gameObject);
        gameObject.transform.position = new Vector3(location[0], location[1], 0);
        gameObject.transform.SetParent(parent.transform);

        Tile tile = gameObject.AddComponent<Tile>();
        tile.Type = type;

        TileDictionary.Add(gameObject.transform.position, tile);
        return gameObject;
    }

    public static bool GetTile(Vector3 position, out Tile Tile)
    {
        if (TileDictionary.TryGetValue(position, out Tile))
        {
            return true;
        }

        return false;

    }

    public void SetTransparency(float transparency)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = transparency;
        sr.color = c;
    }

}

