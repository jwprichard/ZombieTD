using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    int[] GetStats();
    int Cost { get; }
    int Health { get; set; }
    int Range { get; set; }
    float ROF { get; set; }
}
