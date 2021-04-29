using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class MapGenerator
{
    private static GameObject map;
    private static int[,] mapArray;

    public static int[,] GetMap()
    {
        return mapArray;
    }

    public static void CreateMap(int width=30, int height=30)
    {
        CreateTileMap();
        mapArray = GenerateMapArray(width, height);
        SetTiles(mapArray);
    }

    
    /* 
     * Creates a TileMap and adds it
     * to the GameController as a child
     */
    public static Tilemap CreateTileMap()
    {
        map = new GameObject("Map");
        map.AddComponent<Grid>();
        map.transform.SetParent(Object.FindObjectOfType<GameController>().transform);

        var gameObject = new GameObject("TileMap");
        gameObject.transform.localScale = new Vector3(1, 1, 0);
        var tileMap = gameObject.AddComponent<Tilemap>();
        var renderer = gameObject.AddComponent<TilemapRenderer>();

        tileMap.tileAnchor = new Vector3(0, 0, 0);
        gameObject.transform.SetParent(map.transform);
        renderer.sortingLayerName = "Background";
        

        return tileMap;
    }

    private static int[,] GenerateMapArray(int mapWidth, int mapHeight)
    {
        int[,] array = new int[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth-1; x++)
        {
            for (int y = 0; y < mapHeight-1; y++)
            {
                if (x < 2 || y < 2 || x > mapWidth-4 || y > mapHeight - 4)
                {
                    //Mountain
                    array[x, y] = 1;
                }
                else
                {
                    //Grass
                    array[x, y] = 2;
                }
            }
        }

        return array;
    }

    ////Make some walls protrude from the outer wall
    ///Currently not being used!!!
    //private static int[,] MakeStructures(int[,] arrayMap)
    //{
    //    int x = Random.Range(3, arrayMap.GetLength(0) / 5);
    //    int y = Random.Range(3, arrayMap.GetLength(1));

    //    //arrayMap[x, y] = 1;

    //    if (x < y)
    //    {
    //        for (int i = 0; i < x; i++)
    //        {
    //            arrayMap[i, y] = 1;
    //            if(Mathf.Abs(y-arrayMap.GetLength(1)) > y)
    //            {
    //                arrayMap[i , y+1] = 1;
    //            }
    //            else
    //            {
    //                arrayMap[i , y-1] = 1;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < y; i++)
    //        {
    //            arrayMap[x, i] = 1;
    //        }
    //    }
    //    return arrayMap;

    //}

    private static void SetTiles(int[,] arrayMap)
    {
        for (int i = 0; i < arrayMap.GetLength(0); i++)
        {
            for (int j = 0; j < arrayMap.GetLength(1)-1; j++)
            {
                RuleTile tile = CheckCell(arrayMap[i, j]);
                Vector3Int pos = new Vector3Int(i, j, 0);
                map.transform.GetComponentInChildren<Tilemap>().SetTile(pos, tile);
                SetupTile("Tile", i, j);
            }
        }
    }
    
    private static RuleTile CheckCell(int cell)
    {
        if (cell == 1)
        {
            return Resources.Load<RuleTile>("Tiles/Mountain_Tile"); ;
        }
        else if (cell == 2)
        {
            return Resources.Load<RuleTile>("Tiles/Grass_Tile"); ;
        }
        else
        {
            return null;
        }
    }

    static GameObject SetupTile(string type, int location_1, int location_2)
    {
        int[] loc = new int[2];
        loc[0] = location_1;
        loc[1] = location_2;
        GameObject tile = TileScript.CreateTile(type, loc, map);
        return tile;
    }

}
