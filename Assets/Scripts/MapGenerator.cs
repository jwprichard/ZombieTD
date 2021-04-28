using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tileMap;
    private RuleTile grassTile;
    private RuleTile mountainTile;
    private int[,] MapArray;
    public static int mapWidth;
    public static int mapHeight;

    public int[,] GetMap()
    {
        return MapArray;
    }

    public void CreateTileMap()
    {
        mapWidth = 30;
        mapHeight = 30;
        //Vector3Int pos = new Vector3Int(1, 1, 1);
        grassTile =  Resources.Load<RuleTile>("Tiles/Grass_Tile");
        mountainTile = Resources.Load<RuleTile>("Tiles/Mountain_Tile");
        SetTiles(GenerateMapArray());
        
    }

    private int[,] GenerateMapArray()
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

        MapArray = array;
        return array;
    }

    private int[,] MakeSimpleMap(int[,] arrayMap)
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
    private int[,] MakeStructures(int[,] arrayMap)
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

    private void SetTiles(int[,] arrayMap)
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight-1; j++)
            {
                RuleTile tile = CheckCell(arrayMap[i, j]);
                Vector3Int pos = new Vector3Int(i, j, 0);
                tileMap.SetTile(pos, tile);
            }
        }
    }
    
    private RuleTile CheckCell(int cell)
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

    static GameObject setupTile(string type)
    {
        int[] loc = new int[2];
        loc[0] = 1;
        loc[1] = 1;
        GameObject tile = TileScript.CreateTile(type, loc);
        return tile;
    }

}
