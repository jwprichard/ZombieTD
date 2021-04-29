using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public static Dictionary<GameObject, Tile> TileDictionary = new Dictionary<GameObject, Tile>();
    static int idNum = 0;

    public static GameObject CreateTile(string type, int[] location, GameObject parent)
    {
        idNum++;
        GameObject gameObject = Resources.Load<GameObject>("Objects/" + type);
        gameObject = Instantiate(gameObject);
        gameObject.transform.position = new Vector3(location[0], location[1], 0);
        gameObject.transform.SetParent(parent.transform);
        Tile tile = new Tile(gameObject, type, location, idNum);
        TileDictionary.Add(gameObject, tile);
        return gameObject;
    }

    public class Tile
    {
        //public string Name { get; set; }
        public string Type { get; set; }
        public GameObject GameObject { get; }
        public int[] Location = new int[2];

        public Tile(GameObject gameObject, string type, int[] location, int id)
        {
            GameObject = gameObject;
            Type = type;
            GameObject.name = Type + "_" + id;
            Location = location;
        }

    }
}
