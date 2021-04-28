using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UserInterface UI;
    public static bool gameOver = false;

    private static int Money = 500;
    public static int Round = 1;
    public static float time = 0;

    //private string selectedBuilding = "Turret";

    private GameObject selectedBuilding;

    // Start is called before the first frame update
    void Start()
    {
        MapGenerator.CreateMap();
        Camera.main.transform.position = new Vector3(0, 0, -20);
        CameraMovement.UpdatePosition();
        //SetupLocations();
    }

    //Sets up the start locations of the spawner and the base
    void SetupLocations()
    {
        //Base
        int x = Functions.RandomNumber(10, MapGenerator.mapWidth * 4 - 10);
        int y = Functions.RandomNumber(10, MapGenerator.mapHeight * 4 - 10);
        Vector3 pos1 = new Vector3(x, y, -1);
        //Spawner
        ZombieType[] zt = new ZombieType[3];
        zt[0] = ZombieType.Slow_Zombie;
        zt[1] = ZombieType.Fast_Zombie;
        zt[2] = ZombieType.Zombie3;
        x = Functions.RandomNumber(10, MapGenerator.mapWidth * 4 - 10);
        y = Functions.RandomNumber(10, MapGenerator.mapHeight * 4 - 10);
        Vector3 pos2 = new Vector3(x, y, -1);

        //if (Functions.GetDistance(pos1, pos2) < 100)
        //{
        //    Debug.Log($"Position 1: {pos1}, Position 1: {pos2}");
        //    SetupLocations();
        //}
        //else
        //{
        //    selectedBuilding = BuildingScript.BuildingPreview("Base");
        //    BuildingScript.CreateBuilding(selectedBuilding, pos1);
        //    ZombieScript.CreateMonsterSpawner(pos2, 5, 10, zt);
        //    selectedBuilding = BuildingScript.BuildingPreview("Turret");
        //}
        
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
            Vector3 pos = Functions.FindTilePos();
            selectedBuilding.transform.position = new Vector3(pos.x, pos.y, 0);
            Debug.Log($"Mouse Position: {selectedBuilding.transform.position}");
        }
    }

    //Displays game over text and quits the application
    private void GameOver()
    {
        UI.GameOver();
        Application.Quit();
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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Functions.FindTilePos();
            BuildingScript.CreateBuilding(selectedBuilding, pos);
        }

        //On right click
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 20;
            pos = Camera.main.ScreenToWorldPoint(pos);
            Debug.Log($"Mouse Pos: {pos}");
            //ray = new Ray(pos, Vector3.down);
            ZombieScript.CreateZombie(ZombieType.Fast_Zombie, pos);
        }
        if (Input.GetMouseButtonDown(2))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 20;
            pos = Camera.main.ScreenToWorldPoint(pos);
            Debug.Log($"Mouse Pos: {pos}");
            ZombieType[] zt = new ZombieType[2];
            zt[0] = ZombieType.Fast_Zombie;
            zt[1] = ZombieType.Slow_Zombie;
            GameObject go = Resources.Load<GameObject>("Objects/Zombie_Grave");
            go = Instantiate(go);
            go.transform.position = pos;
            //ZombieSpawner spawner =  new ZombieSpawner(10, zt);
            

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selectedBuilding = null;
        }
    }


}
