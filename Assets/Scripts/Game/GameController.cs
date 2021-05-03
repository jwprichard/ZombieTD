using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UserInterface UI;
    public static bool gameOver = false;

    public static System.Random rnd = new System.Random();

    private static int Money = 500;
    public static int Round = 1;
    public static float time = 0;

    //private string selectedBuilding = "Turret";

    private GameObject selectedBuilding;

    // Start is called before the first frame update
    void Start()
    {
        MapGenerator.CreateMap(30, 30, 0);
        
        Camera.main.transform.position = new Vector3(0, 0, -20);
        CameraMovement.UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        time += Time.deltaTime;
        UpdateGameBarValues();
        if (gameOver)
        {
            GameOver();
        }
        //Functions.FindTilePos();
        UpdateBuildingPreviewPosition();
    }

    //Displays game over text and quits the application
    private void GameOver()
    {
        UI.GameOver();
        Application.Quit();
    }


    //Updates the values in the game bar
    private void UpdateGameBarValues()
    {
        UI.UpdateCash(Money);
        UI.UpdateRound(Round);
        UI.UpdateTime((int)time);
    }

    private void UpdateBuildingPreviewPosition()
    {
        if (selectedBuilding != null) 
        {
            Vector3 pos = Functions.FindMouseTile().transform.position;
            selectedBuilding.transform.position = new Vector3(pos.x, pos.y, 0);
        }
    }

    //Set the selected object to build
    public void SetSelected(string selected)
    {
        selectedBuilding = BuildingScript.BuildingPreview(selected);
    }

    //Add money to the total money count
    public static void AdjustMoney(int money)
    {
        Money += money;
    }

    public static int GetMoney()
    {
        return Money;
    }

    //Check for inputs by player
    private void CheckInput()
    {

        //On left click
        if (Input.GetMouseButtonDown(0) && selectedBuilding != null)
        {
            Tile tile = Functions.FindMouseTile();
            BuildingScript.CreateBuilding(selectedBuilding.name, tile);
            Destroy(selectedBuilding);
            selectedBuilding = null;
        }

        //On right click
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 20;
            pos = Camera.main.ScreenToWorldPoint(pos);
            ZombieScript.CreateZombie(ZombieType.Fast_Zombie, pos);
        }
        if (Input.GetMouseButtonDown(2))
        {
            //TileScript.Tile tile = Functions.FindTile();
            //int[] array = tile.GetBuilding().GetStats();
            //foreach(int val in array)
            //{
            //    Debug.Log($"Value is: {val}");
            //}
            //if(TileScript.GetTile(new Vector3(5, 5, 0), out Tile tile))
            //{
            //    SpriteRenderer sr = tile.GetComponent<SpriteRenderer>(); //Store the Sprite Renderer
            //    Color tmp = sr.color; //Store the color
            //    tmp.a = 0.5f; //Change the alpha
            //    sr.color = tmp; //Change the color
            //}

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(selectedBuilding);
            selectedBuilding = null;
        }
    }


}
