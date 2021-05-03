using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    int[] GetStats();
    int cost { get; }
    int Health { get; set; }
}
