using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

