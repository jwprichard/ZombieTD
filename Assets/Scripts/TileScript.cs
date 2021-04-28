using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public static Dictionary<GameObject, Tile> TileDictionary = new Dictionary<GameObject, Tile>();
    static int idNum = 0;

    public static GameObject CreateTile(string type, int[] location)
    {
        idNum++;
        GameObject gameObject = Resources.Load<GameObject>("Objects/" + type);
        GameObject newTile = Instantiate(gameObject);
        newTile.transform.position = new Vector3(location[0], location[1], 0);
        Tile tile = new Tile(newTile, type, location, idNum);
        TileDictionary.Add(newTile, tile);
        return newTile;
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
            //GameObject.GetComponent<SpriteRenderer>().sprite = GetSprite(this);
        }

    }
}
