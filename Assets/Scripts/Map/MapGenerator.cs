using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class MapGenerator
{
    private static GameObject map;
    private static string[,] mapArray;

    public static string[,] GetMap()
    {
        return mapArray;
    }

    public static void CreateMap(int width=30, int height=30, int num_spawners = 3)
    {
        CreateTileMap();
        mapArray = GenerateMapArray(width, height, num_spawners);
        SetTiles(mapArray);
        PlaceHub();
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

    private static string[,] GenerateMapArray(int mapWidth, int mapHeight, int num_spawners)
    {
        string[,] array = new string[mapWidth, mapHeight];

        bool all_spawned = false;
        int num_spawned = 0;

        for (int x = 0; x < mapWidth-1; x++)
        {
            for (int y = 0; y < mapHeight-1; y++)
            {
                if (x < 2 || y < 2 || x > mapWidth-4 || y > mapHeight - 4)
                {
                    //Mountain
                    array[x, y] = "Mountain_Tile";
                }
                else
                {
                    //Grass
                    int num = Functions.RandomNumber(1, 100);
                    if (num > 50 && !all_spawned && num_spawners != 0)
                    {
                        array[x, y] = "Spawner";
                        num_spawned++;
                        if (num_spawned == num_spawners)
                        {
                            all_spawned = true;
                        }
                    }
                    else
                    {
                        array[x, y] = "Grass_Tile";
                    }
                }
            }
        }

        

        return array;
    }

    private static void SetTiles(string[,] arrayMap)
    {
        for (int i = 0; i < arrayMap.GetLength(0)-1; i++)
        {
            for (int j = 0; j < arrayMap.GetLength(1)-1; j++)
            {
                RuleTile tile;
                Vector3Int pos = new Vector3Int(i, j, 0);
                if (arrayMap[i, j].Contains("Tile")){
                    tile = Resources.Load<RuleTile>("Tiles/" + arrayMap[i, j]);
                }
                else
                {
                    ZombieScript.CreateMonsterSpawner(pos, 5, 10);
                    tile = Resources.Load<RuleTile>("Tiles/Grass_Tile");
                }
                map.transform.GetComponentInChildren<Tilemap>().SetTile(pos, tile);
                SetupTile("Tile", i, j);
            }
        }
    }

    private static void PlaceHub()
    {
        Vector3 HubPosition = new Vector3(mapArray.GetLength(0) / 2, mapArray.GetLength(1) / 2, 0);
        if(Tile.GetTile(HubPosition, out Tile tile))
        {
            BuildingScript.CreateBuilding("Base", tile);
        }
        Functions.SetFogOfWar(HubPosition, 5);
    }

    static GameObject SetupTile(string type, int x, int y)
    {
        int[] loc = new int[2];
        loc[0] = x;
        loc[1] = y;
        GameObject tile = Tile.CreateTile(type, loc, map);
        return tile;
    }

}
