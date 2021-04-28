using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class MapGenerator
{
    public static Tilemap tileMap;
    private static RuleTile grassTile;
    private static RuleTile mountainTile;
    private static int[,] mapArray;
    public static int mapWidth;
    public static int mapHeight;

    public static int[,] GetMap()
    {
        return mapArray;
    }

    public static void CreateMap(int width=30, int height=30)
    {
        mapWidth = width;
        mapHeight = height;
        tileMap = CreateTileMap();
        grassTile = Resources.Load<RuleTile>("Tiles/Grass_Tile");
        mountainTile = Resources.Load<RuleTile>("Tiles/Mountain_Tile");
        mapArray = GenerateMapArray();
        SetTiles(mapArray);
    }

    
    /* 
     * Creates a TileMap and adds it
     * to the GameController as a child
     */
    public static Tilemap CreateTileMap()
    {
        var go = new GameObject("Grid");
        var grid = go.AddComponent<Grid>();
        go.transform.SetParent(Object.FindObjectOfType<GameController>().transform);

        var gameObject = new GameObject("TileMap");
        gameObject.transform.localScale = new Vector3(1, 1, 0);
        var tileMap = gameObject.AddComponent<Tilemap>();
        var renderer = gameObject.AddComponent<TilemapRenderer>();

        tileMap.tileAnchor = new Vector3(0, 0, 0);
        gameObject.transform.SetParent(go.transform);
        renderer.sortingLayerName = "Background";
        

        return tileMap;
    }

    private static int[,] GenerateMapArray()
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

    private static int[,] MakeSimpleMap(int[,] arrayMap)
    {
        //int numOfPoints = Random.Range(2, 7);
        int numOfPoints = 4;
        int[,] points = new int[numOfPoints,2];

        for (int i = 0; i < numOfPoints; i++)
        {
            points[i,0] = Random.Range(3, mapWidth - 3);
            points[i,1] = Random.Range(3, mapHeight - 3);
            int length = Random.Range(3, 7);
            int height = Random.Range(2, 6);

            for (int x = 0; x < length; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayMap[points[i, 0]+x, points[i, 1]+y] = 1;
                    //arrayMap[points[1, 0]+x, points[1, 1]+y] = 1;
                }
            }


        }

        return arrayMap;
    }

    //Make some walls protrude from the outer wall
    private static int[,] MakeStructures(int[,] arrayMap)
    {
        int x = Random.Range(3, mapWidth / 5);
        int y = Random.Range(3, mapHeight);

        //arrayMap[x, y] = 1;

        if (x < y)
        {
            for (int i = 0; i < x; i++)
            {
                arrayMap[i, y] = 1;
                if(Mathf.Abs(y-mapHeight) > y)
                {
                    arrayMap[i , y+1] = 1;
                }
                else
                {
                    arrayMap[i , y-1] = 1;
                }
            }
        }
        else
        {
            for (int i = 0; i < y; i++)
            {
                arrayMap[x, i] = 1;
            }
        }
        return arrayMap;

    }

    private static void SetTiles(int[,] arrayMap)
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight-1; j++)
            {
                RuleTile tile = CheckCell(arrayMap[i, j]);
                Vector3Int pos = new Vector3Int(i, j, 0);
                tileMap.SetTile(pos, tile);
                SetupTile("Red", i, j);

            }
        }
    }
    
    private static RuleTile CheckCell(int cell)
    {
        if (cell == 1)
        {
            return mountainTile;
        }
        else if (cell == 2)
        {
            return grassTile;
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
        GameObject tile = TileScript.CreateTile(type, loc);
        return tile;
    }

}
