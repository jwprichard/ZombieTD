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

        Tile tile = gameObject.AddComponent<Tile>();
        tile.Type = type;

        TileDictionary.Add(gameObject, tile);
        return gameObject;
    }



    public class Tile : MonoBehaviour
    {
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



    }
}
